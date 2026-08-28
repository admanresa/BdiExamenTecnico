# BdiExamen

Aplicación de mantenimiento de un catálogo (altas, bajas, modificaciones) construida en varias capas, desarrollada como **una sola solución de Visual Studio con varios proyectos**, tal como lo pide el examen.

La arquitectura general es:

```
Front (escritorio o web)
   └── Librería apiexamen.dll  (configurable: Stored Procedures o WebService)
         └── WebService (Entity Framework, transaccional)
                └── Base de datos SQL Server
```

---

## Mapa de las tareas del examen

| Tarea | Qué pide | Dónde está resuelto |
|------|----------|---------------------|
| **Tarea 1 – Capa de base de datos** | Crear BD `BdiExamen`, tabla `tblExamen` y los SP `spAgregar`, `spActualizar`, `spEliminar`, `spConsultar` | Carpeta `1 Scripts\` |
| **Tarea 2 – WebService** | WebService que guarda/consulta con **Entity Framework** y **transaccionalidad** (sin usar los SP) | `2 Solution\BdiExamen.WsApiexamen` + `BdiExamen.DAL` + `BdiExamen.Model` |
| **Tarea 3 – Librería de acceso** | Ensamblado `apiexamen.dll` con la clase `clsExamen`, configurable para usar **SP o WebService**, con validación de datos de entrada | `2 Solution\BdiExamen.ApiExamen` |
| **Tarea 4 – Pantalla de entrada de datos** | Front (escritorio o web) con captura, botones ABC, grilla con líneas pares/impares diferenciadas y visualización de errores/éxito. Se entrega un front de **escritorio (WinForms)** y uno **web (MVC)** | `2 Solution\BdiExamen.WinFormsExamen` y `2 Solution\BdiExamen.MvcExamen` |

---

## Estructura de archivos

```
BdiExam/
├── README.md
│
├── 1 Scripts/                      ← TAREA 1: Base de datos
│   ├── 01_CreateDatabase_BdiExamen.sql   Creación de la BD BdiExamen
│   ├── 02_CreateTable_tblExamen.sql      Script de la tabla tblExamen
│   ├── 03_spAgregarExamen.sql            Alta (INSERT) con código y mensaje de retorno
│   ├── 04_spActualizarExamen.sql         Actualización (UPDATE) idem
│   ├── 05_spEliminarExamen.sql           Baja (DELETE) idem
│   ├── 06_spConsultarExamenes.sql        Consulta filtrada por Nombre/Descripción
│   └── 07_PopulateData_tblExamen.sql     Datos de prueba opcionales
│
└── 2 Solution/                    ← Una única solución de Visual Studio
    ├── BdiExamen.slnx                    Archivo de solución
    │
    ├── BdiExamen.Model/           ← Proyecto "compartido" (Tareas 2, 3 y 4)
    │   ├── Entities/Examen.cs            Entidad que representa la tabla tblExamen
    │   ├── DTOs/OperationResult.cs       Resultado: Exitoso + CódigoRetorno + Descripción
    │   ├── DTOs/AgregarResult.cs         OperationResult + IdGenerado
    │   ├── DTOs/ConsultaResult.cs        Resultado + colección de Examen
    │   └── Enum/ModoAcceso.cs            StoredProcedure | WebService
    │
    ├── BdiExamen.DAL/             ← Acceso a datos con Entity Framework (Tarea 2)
    │   ├── ExamenContext.cs              Contexto EF (mapea la tabla existente)
    │   ├── ExamenRepository.cs           CRUD con transacciones BeginTransaction/Commit/Rollback
    │   ├── IExamenRepository.cs          Contrato del repositorio
    │   └── Mappings/ExamenMap.cs         Configuración de mapeo de la entidad
    │
    ├── BdiExamen.ApiExamen/       ← apiexamen.dll: Librería de acceso (Tarea 3)
    │   ├── clsExamen.cs                  Clase principal: métodos Agregar/Actualizar/Eliminar/Consultar
    │   ├── ExamenGatewayFactory.cs       Fábrica que crea el gateway según ModoAcceso
    │   ├── Gateways/
    │   │   ├── IExamenGateway.cs         Contrato común (SP o WebService)
    │   │   ├── SpExamenGateway.cs        Llama a los Stored Procedures con ADO.NET
    │   │   └── WebServiceExamenGateway.cs  Consume el WebService vía HttpClient
    │   └── Validation/ExamenValidator.cs Valida los datos de entrada (no muestra mensajes)
    │
    ├── BdiExamen.WsApiexamen/     ← WebService Web API (Tarea 2)
    │   ├── Controllers/ExamenController.cs  Expone Agregar/Actualizar/Eliminar/Consultar
    │   ├── Models/ExamenRequest.cs       Modelo de entrada del API
    │   ├── App_Start/WebApiConfig.cs     Rutas del API
    │   ├── index.html                    Página informativa de los endpoints
    │   └── Web.config                     Cadena de conexión del contexto EF
    │
    ├── BdiExamen.WinFormsExamen/  ← Front de escritorio (Tarea 4)
    │   ├── frmExamen.cs                  Pantalla con TextBox, botones ABC y DataGridView
    │   ├── frmExamen.Designer.cs         Layout de la pantalla
    │   ├── Services/ExamenService.cs     Consume la clase clsExamen (apiexamen.dll)
    │   └── Program.cs                    Punto de entrada
    │
    ├── BdiExamen.MvcExamen/       ← Front web ASP.NET MVC (Tarea 4, alternativa)
    │   ├── Controllers/ExamenController.cs  Acciones Index/Create/Edit/Delete
    │   ├── Views/Examen/                 Vistas Index, Create, Edit, Delete
    │   ├── Services/ExamenApiClient.cs   Cliente HTTP que consume el WebService
    │   └── Models/ExamenViewModel.cs     ViewModels de listado y formulario
    │
    └── packages/                  ← Paquetes NuGet (EF 6, MVC 5, Web API 2, Newtonsoft.Json)
```

---

## Descripción y alineación de cada proyecto con la tarea

### Tarea 1 – Capa de base de datos (`1 Scripts\`)

Scripts SQL autónomos y re-ejecutables (idempotentes) que crean todo en orden:

- `01_` → crea la base de datos `BdiExamen` si no existe.
- `02_` → crea `tblExamen` (`Id` IDENTITY, `Nombre` NVARCHAR(100), `Descripcion` NVARCHAR(500)).
- `03_` a `06_` → los 4 stored procedures:
  - **spAgregarExamen** → INSERT; devuelve el Id generado, `@CodigoRetorno` y `@DescripcionRetorno` (0 y mensaje en éxito, número y texto del error en fallo).
  - **spActualizarExamen** → UPDATE sobre el Id recibido.
  - **spEliminarExamen** → DELETE sobre el Id recibido.
  - **spConsultarExamenes** → SELECT filtrado por Id/Nombre/Descripción (cualquiera opcional).
- `07_` → datos de prueba.

Nombres de procedimientos ligeramente distintos a los del enunciado (`spAgregarExamen` en lugar de `spAgregar`), manteniendo la misma funcionalidad.

### Tarea 2 – WebService (`BdiExamen.WsApiexamen`)

WebService implementado como **Web API 2 (REST)** (la opción que permite el enunciado). Funciones expuestas:

| Función | Método / URL | Descripción |
|---------|-------------|-------------|
| `AgregarExamen` | POST `/api/examen/agregar` | Inserta con EF |
| `ActualizarExamen` | PUT `/api/examen/actualizar` | Actualiza con EF |
| `EliminarExamen` | DELETE `/api/examen/eliminar/{id}` | Elimina con EF |
| `ConsultarExamen` | GET `/api/examen/consultar?Id=&Nombre=&Descripcion=` | Consulta con EF |

Detalles pedidos y cómo se cumple:
- **Entity Framework**: el API usa `ExamenRepository`/`ExamenContext` (`BdiExamen.DAL`), que mapea la tabla existente sin `Database.Create`.
- **Sin usar los stored procedures**: el contexto EF trabaja directo sobre `tblExamen`.
- **Transaccionalidad**: `context.Database.BeginTransaction()` con `Commit`/`Rollback` en cada operación.
- **Salida**: boolean (`Exitoso`) + descripción de retorno, vía los DTOs de `BdiExamen.Model`.

### Tarea 3 – Librería de acceso (`BdiExamen.ApiExamen`)

Genera el ensamblado **`apiexamen.dll`** (`AssemblyName = apiexamen`). Contiene la clase **`clsExamen`**, que es el corazón de la tarea:

- **Configurable SP o WebService**: el constructor recibe un `ModoAcceso` (`StoredProcedure` o `WebService`); la `ExamenGatewayFactory` elige el gateway correspondiente.
- **Por Stored Procedures**: `SpExamenGateway` ejecuta los SP con ADO.NET (`SqlConnection`/`SqlCommand`).
- **Por WebService**: `WebServiceExamenGateway` consume los endpoints del API con `HttpClient`.
- **Validación de entrada**: `ExamenValidator` valida antes de persistir; `clsExamen` nunca muestra mensajes en pantalla, solo los devuelve en los objetos de resultado.
- **Transaccionalidad**: el camino por SP usa `SqlTransaction` y el camino por WebService delega en que el API usa transacciones EF.

### Tarea 4 – Pantalla de entrada de datos

Se entregaron **dos frentes** (el enunciado pedía elegir uno):

- **`BdiExamen.WinFormsExamen`** (escritorio): consume directamente la librería `apiexamen.dll` a través de `Services\ExamenService`. Tiene TextBox de captura (Id, Nombre, Descripción), botones **Nuevo / Guardar / Eliminar / Consultar / Limpiar**, un `DataGridView` con la grilla de resultados y una barra de estado que informa en verde/rojo el éxito o el error de cada operación.
- **`BdiExamen.MvcExamen`** (web): alternativa que consume el WebService a través de `Services\ExamenApiClient`. Páginas **Index** (grilla + filtros de consulta), **Create**, **Edit** y **Delete**, mostrando mensajes de éxito/error mediante alertas.

Ambos muestran en pantalla los errores y el éxito de las operaciones (la validación la hace la librería, el front solo los presenta).

---

## Datos de conexión

| Dato | Valor |
|------|-------|
| Servidor | `bsiexamendes\SQLEXPRESS` |
| Autenticación | Windows Integrated |
| Usuario de la máquina | `examenlocal` / `Desarrollo01` |
| Base de datos | `BdiExamen` |

Las configuraciones necesarias (contienen `Data Source=.\SQLExpress`, local) están **por proyecto** en:

| Proyecto | Archivo | Clave | Para qué se usa | ¿Obligatoria? |
|----------|---------|-------|-----------------|---------------|
| `BdiExamen.WsApiexamen` | `Web.config` | `ExamenContext` | Conexión WebService → BD (la usa `ExamenContext`/EF en `BdiExamen.DAL`) | Sí |
| `BdiExamen.WinFormsExamen` | `App.config` | `ExamenSqlDirecto` | Conexión del front de escritorio en modo **Stored Procedures** (la lee `SpExamenGateway` de `apiexamen.dll` desde la configuración del ejecutable) | Sí (para modo SP) |
| `BdiExamen.MvcExamen` | `Web.config` | `WsApiexamenBaseUrl` | URL donde está publicado el WebService (la lee `ExamenController`) | Sí |

Notas:
- `BdiExamen.ApiExamen\App.config` también define `ExamenSqlDirecto` y `WsApiexamenBaseUrl`, pero **no se usa en tiempo de ejecución**: al ser una librería, `ConfigurationManager` lee la configuración de la aplicación anfitriona (el `.exe` o `web.config`), no la del `.dll`. Por eso no se lista.
- Si se quisiera el modo **WebService** desde el WinForms, agregar la clave `WsApiexamenBaseUrl` en `BdiExamen.WinFormsExamen\App.config`.
- Si el servidor real difiere de `.\SQLExpress`, ajustar las claves de la tabla según el proyecto antes de ejecutar.

---

## Cómo compilar y ejecutar

1. **Base de datos**: ejecutar los scripts de `1 Scripts\` en orden numerado (`01` → `07`).
2. **WebService**: abrir `2 Solution\BdiExamen.slnx`, publicar/ejecutar `BdiExamen.WsApiexamen` (URL por defecto `https://localhost:44352`). Verificar que la `WsApiexamenBaseUrl` del front consumido (`BdiExamen.MvcExamen\Web.config`, o agregarla en `BdiExamen.WinFormsExamen\App.config` para el modo WebService) coincida con la URL del WebService.
3. **Front**: ejecutar `BdiExamen.WinFormsExamen` (escritorio) o `BdiExamen.MvcExamen` (web).
4. El modo de acceso (SP/WebService) se selecciona al crear `clsExamen` (`ModoAcceso`).

Antes de subir al repositorio conviene añadir un `.gitignore` para excluir `bin\`, `obj\`, `.vs\` y `packages\`.