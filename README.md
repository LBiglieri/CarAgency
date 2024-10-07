# CarAgency

CarAgency es un sistema diseñado para gestionar la compra y venta de automóviles, desarrollado en C# con SQL Server como base de datos. Este sistema permite la gestión eficiente de usuarios, roles y transacciones, con una arquitectura sólida que sigue patrones de diseño para garantizar su escalabilidad y mantenimiento.

##Características Principales
*Login Seguro con Patrón Singleton: Implementación del patrón Singleton para garantizar una única instancia del sistema de autenticación, asegurando un control centralizado del login de usuarios.
*Multiidioma con Patrón Observer: Sistema adaptable que permite cambiar el idioma dinámicamente según las preferencias del usuario. El patrón Observer asegura que todos los módulos y componentes de la interfaz se actualicen automáticamente cuando se cambia el idioma.
*Gestión de Roles con Patrón Composite: Manejo de roles de usuarios (administradores, vendedores, compradores, etc.) mediante el patrón Composite, permitiendo una estructura jerárquica y flexible de permisos y acceso a las funcionalidades del sistema.
*SQL Server como Base de Datos: Toda la información sobre usuarios, roles, transacciones de compra y venta, inventario de vehículos, y otros datos críticos se almacenan en una base de datos SQL Server.

![image](https://github.com/user-attachments/assets/612f291b-c84b-4baa-a268-e962278c0c73)
