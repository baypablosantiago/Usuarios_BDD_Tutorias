# Como evitar Inyecciones SQL

Este es un proyecto utilizado en las tutorias de la Tecnicatura Universitaria en Programacion (TUP), Universidad Tecnologica Nacional (UTN) - Facultad Regional Paraná.

A traves de una sencilla aplicacion de escritorio se busca ejemplificar algunas de las malas practicas al momento de crear codigo, sus consecuencias y como evitarlas. Particularmente se trabajó con las inyecciones SQL, pero tambien se trataron las string connections y el appsetting standart de .NET Core y posteriores, adaptado a .NET Framework.

### Ficha Tecnica

- Desarrollado en **[.NET - Windows Form.](https://learn.microsoft.com/es-es/dotnet/desktop/winforms/?view=netdesktop-9.0)**
- Base de Datos embebida - **[SQLite.](https://www.sqlite.org/)**

## 1. Guia de uso para estudiantes

**Nota importante:** siguiendo la misma linea de trabajo dada por los docentes de Programacion 1 y 2 , se recomienda el uso de Visual Studio 2022.

1. Clonar el repositorio utilizando la consola.
   ```sh
   git clone https://github.com/baypablosantiago/Usuarios_BDD_Tutorias.git
    ```
2. F5 para jecutar el proyecto. La primera vez que se ejecuta crea la tabla y se insertan los usuarios para hacer los tests. Si necesitas volver a insertar los usuarios en caso de haberlos eliminado, la forma mas sencilla es repetir el punto 1.
   ```sh
   git clone https://github.com/baypablosantiago/Usuarios_BDD_Tutorias.git
    ```

![imagen](https://github.com/user-attachments/assets/4b607a63-9612-4795-b1d5-55305ec4f95a)

3. La pestaña principal posee un textBox de usuario y otro de contraseña, disparandose una ventana modal con la informacion privada del usuario en caso de un login correcto o un mensaje de error en un login fallido. **Los usuarios se encuentran hardcodeados en el codigo.**

![imagen](https://github.com/user-attachments/assets/d70b268e-1481-4d26-877b-88bac827dc33)

4. En el siguiente apartado estan las querys para ingresar en los textBox de la aplicacion y ver los resultados.
El codigo se encuentra totalmente comentado, con las aclaraciones sobre que medidas de seguridad se utilizan para evitar estas vulneraciones de seguridad.

## 2. Inyecciones utilizadas

1. Bypass de Autenticación General
   ```sh
   ' OR 1=1 --
    ```
    
2. Bypass de Autenticación 2 con usuario target.
   ```sh
   rosa' --
    ```

3. Extraccion de la contraseña de un usuario target.
   ```sh
   --' UNION SELECT userpass AS username, '123' userpass, 'Lo que esta despues de Bienvenid@ es la password de juan' sensitive_information FROM USERS WHERE username = 'juan' ORDER BY username DESC; --
    ```

4. Eliminacion de todos los usuarios en la base de datos.
   ```sh
   '; DELETE FROM USERS; --
    ```
 
5. Creacion de un usuario malicioso.
   ```sh
   '; INSERT INTO USERS (USERNAME, USERPASS, SECRET) VALUES ('1', '1', 'Su App fue vulnerada.'); --
    ```  
    
## 3. Nota final

>[!NOTE] Importante
> Hoy en dia es poco probable encontrar una app que llegue a ser vulnerable a este tipo de ataques,
> pero igualmente la idea de esta clase fue dar ejemplo de la importancia de general un codigo
> de calidad y seguro.

