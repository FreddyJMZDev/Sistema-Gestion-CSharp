# Sistema de Gestión de Registros en C#

Proyecto educativo de consola con **C#**, **.NET Framework 4.8** y **programación orientada a objetos**. Permite administrar registros de nombre y correo.

## Funcionalidades

- Agregar, listar, buscar, editar y eliminar registros (CRUD).
- Validar nombre y formato básico de correo.
- Guardar y recuperar datos en un archivo JSON local.

## Ejecutar

1. Instalar Visual Studio con la carga de trabajo **Desarrollo de escritorio de .NET** y el paquete de destino de **.NET Framework 4.8**.
2. Abrir `SistemaGestion.sln`.
3. Compilar y ejecutar con **Ctrl + F5**.
4. Utilizar el menú numérico.

El archivo `Datos/registros.json` se crea automáticamente junto al ejecutable (normalmente en `bin/Debug/Datos`). No se sube al repositorio.

## Estructura

- `Modelos/Registro.cs`: entidad de datos.
- `Servicios/RegistroService.cs`: operaciones CRUD y almacenamiento.
- `Servicios/ValidadorRegistro.cs`: validación de campos.
- `Program.cs`: menú de consola.
- `Pruebas/PruebasManuales.md`: lista de casos de prueba por ejecutar.

## Limitaciones

Proyecto de aprendizaje diseñado para un único usuario y almacenamiento local. No incluye base de datos, autenticación ni pruebas automatizadas. El guardado JSON no es transaccional; se recomienda respaldar datos importantes.

**Estado:** En desarrollo.
