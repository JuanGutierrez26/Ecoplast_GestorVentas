CREATE DATABASE EcoPlastic2;
GO

USE EcoPlastic2;

CREATE TABLE Marca (
    id_marca NVARCHAR(5) PRIMARY KEY,
    nombre NVARCHAR(100) NOT NULL
);

CREATE TABLE Proveedores (
    id_proveedor NVARCHAR(5) PRIMARY KEY,
    nombre NVARCHAR(100) NOT NULL,
    telefono NVARCHAR(20),
    direccion NVARCHAR(255)
);

CREATE TABLE Administrador (
    id_administrador NVARCHAR(5) PRIMARY KEY,
    nombre NVARCHAR(100) NOT NULL,
    contrasena NVARCHAR(255) NOT NULL -- Asegúrate de almacenar la contraseña de manera segura
);

CREATE TABLE Categoria (
    id_categoria NVARCHAR(5) PRIMARY KEY,
    nombre NVARCHAR(100) NOT NULL
);

CREATE TABLE Productos (
    id_producto NVARCHAR(6) PRIMARY KEY,
    nombre NVARCHAR(100) NOT NULL,
    descripcion NVARCHAR(MAX),
    precio DECIMAL(10, 2) NOT NULL CHECK (precio > 0),
    stock INT NOT NULL CHECK (stock >= 0),
    id_marca NVARCHAR(5),
    id_proveedor NVARCHAR(5),
    id_categoria NVARCHAR(5) NULL,
    FOREIGN KEY (id_marca) REFERENCES Marca(id_marca),
    FOREIGN KEY (id_proveedor) REFERENCES Proveedores(id_proveedor),
    FOREIGN KEY (id_categoria) REFERENCES Categoria(id_categoria)
);

CREATE TABLE Cliente (
    id_cliente NVARCHAR(6) PRIMARY KEY,
    nombre NVARCHAR(100) NOT NULL,
    email NVARCHAR(100) UNIQUE,
    telefono NVARCHAR(20),
    direccion NVARCHAR(255)
);

CREATE TABLE Empleado (
    id_empleado NVARCHAR(5) PRIMARY KEY,
    nombre NVARCHAR(100) NOT NULL,
    email NVARCHAR(100) UNIQUE,
    telefono NVARCHAR(20)
);

CREATE TABLE Venta (
    id_venta NVARCHAR(5) PRIMARY KEY,
    fecha DATETIME NOT NULL,
    id_cliente NVARCHAR(6),
    id_empleado NVARCHAR(5),
    FOREIGN KEY (id_cliente) REFERENCES Cliente(id_cliente),
    FOREIGN KEY (id_empleado) REFERENCES Empleado(id_empleado)
);

CREATE TABLE Detalle_Venta (
    id_detalle INT IDENTITY(1,1) PRIMARY KEY,  -- Usamos IDENTITY para que se auto-incremente
    id_venta NVARCHAR(5),  -- Ahora tiene el mismo tipo que en la tabla Venta
    id_producto NVARCHAR(6),  -- Asegúrate de que este tipo coincida con la clave primaria en la tabla Productos
    cantidad INT NOT NULL CHECK (cantidad > 0),
    precio_unitario DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (id_venta) REFERENCES Venta(id_venta),
    FOREIGN KEY (id_producto) REFERENCES Productos(id_producto)
);




INSERT INTO Marca (id_marca, nombre) VALUES 
('M0001', 'EcoPack'),
('M0002', 'PlasticoPlus'),
('M0003', 'GreenBox'),
('M0004', 'BioContainers'),
('M0005', 'Durapack'),
('M0006', 'Reutilizables Perú'),
('M0007', 'Resistant'),
('M0008', 'EcoWare'),
('M0009', 'PlastiKing'),
('M0010', 'EnvasaTodo');


INSERT INTO Proveedores (id_proveedor, nombre, telefono, direccion) VALUES
('P0001', 'Comercial Andina', '123456789', 'Calle 1, Lima'),
('P0002', 'Distribuidora del Pacífico', '987654321', 'Calle 2, Lima'),
('P0003', 'Suministros Martínez', '123123123', 'Avenida 3, Callao'),
('P0004', 'Industrias Omega', '321321321', 'Calle 4, Arequipa'),
('P0005', 'Servicios Proveplast', '555666777', 'Avenida 5, Trujillo'),
('P0006', 'GlobalPlastics', '999888777', 'Calle 6, Cusco'),
('P0007', 'Distribuidora Sureña', '888777666', 'Avenida 7, Piura'),
('P0008', 'Plastics del Oriente', '777666555', 'Calle 8, Iquitos'),
('P0009', 'Provee Norte', '666555444', 'Avenida 9, Chiclayo'),
('P0010', 'Suministros Andinos', '555444333', 'Calle 10, Tacna');



INSERT INTO Administrador (id_administrador, nombre, contrasena) VALUES 
('A0001', 'admin', 'admin'),
('A0002', 'Juan Pérez', 'contraseñaSegura123'),
('A0003', 'Ana Gómez', 'miContrasena456'),
('A0004', 'Carlos Martínez', 'claveSegura789');


INSERT INTO Categoria (id_categoria, nombre) VALUES 
('C0001', 'Envases'),
('C0002', 'Embalajes'),
('C0003', 'Componentes Industriales'),
('C0004', 'Desechables'),
('C0005', 'Reutilizables'),
('C0006', 'Biodegradables'),
('C0007', 'Contenedores'),
('C0008', 'Tapas'),
('C0009', 'Botellas'),
('C0010', 'Cubiertos');


INSERT INTO Productos (id_producto, nombre, descripcion, precio, stock, id_marca, id_proveedor, id_categoria) VALUES
('PRD001', 'Envase de 500ml', 'Envase plástico resistente de 500ml', 1.50, 100, 'M0001', 'P0001', 'C0001'),
('PRD002', 'Bolsa para embalar', 'Bolsa de alta resistencia', 0.30, 200, 'M0002', 'P0002', 'C0002'),
('PRD003', 'Tapa para envase', 'Tapa ajustable para envases de 500ml', 0.10, 300, 'M0003', 'P0008', 'C0003'),
('PRD004', 'Botella de 1L', 'Botella reutilizable de 1 litro', 2.00, 150, 'M0004', 'P0009', 'C0004'),
('PRD005', 'Caja de 1kg', 'Caja para productos de hasta 1kg', 1.20, 100, 'M0005', 'P0002', 'C0005'),
('PRD006', 'Envase biodegradable', 'Envase compostable de 750ml', 1.80, 200, 'M0006', 'P0006', 'C0006'),
('PRD007', 'Cubiertos de plástico', 'Cubiertos resistentes y desechables', 0.50, 400, 'M0007', 'P0010', 'C0007'),
('PRD008', 'Contenedor de alimentos', 'Contenedor hermético de 2L', 3.00, 120, 'M0008', 'P0007', 'C0008'),
('PRD009', 'Vaso de plástico', 'Vaso desechable de 300ml', 0.20, 500, 'M0009', 'P0004', 'C0009'),
('PRD010', 'Envase para bebidas', 'Envase de 600ml para bebidas frías', 1.00, 250, 'M0010', 'P0001', 'C0010');


INSERT INTO Cliente (id_cliente, nombre, email, telefono, direccion) VALUES
('CL0001', 'Luis Alvarez', 'lalvarez@example.com', '998877665', 'Avenida Principal, Lima'),
('CL0002', 'Ana Ríos', 'arios@example.com', '998877664', 'Calle Secundaria, Arequipa'),
('CL0003', 'Fernando Peña', 'fpena@example.com', '998877663', 'Pasaje Central, Cusco'),
('CL0004', 'Carolina Méndez', 'cmendez@example.com', '998877662', 'Jirón Norte, Callao'),
('CL0005', 'Ricardo Huerta', 'rhuerta@example.com', '998877661', 'Avenida Sur, Trujillo'),
('CL0006', 'Natalia Reyes', 'nreyes@example.com', '998877660', 'Calle Oeste, Piura'),
('CL0007', 'Elena Contreras', 'econtreras@example.com', '998877659', 'Avenida Este, Iquitos'),
('CL0008', 'Juan Morales', 'jmorales@example.com', '998877658', 'Calle Central, Chiclayo'),
('CL0009', 'Miguel Saavedra', 'msaavedra@example.com', '998877657', 'Avenida Norte, Tacna'),
('CL0010', 'Rosa Vargas', 'rvargas@example.com', '998877656', 'Jirón Sur, Moquegua');


INSERT INTO Empleado (id_empleado, nombre, email, telefono) VALUES
('E0001', 'Carlos Mendoza', 'cmendoza@example.com', '999111222'),
('E0002', 'Lucía Ortega', 'lortega@example.com', '999333444'),
('E0003', 'María Paredes', 'mparedes@example.com', '999555666'),
('E0004', 'Jorge Ruiz', 'jruiz@example.com', '999777888'),
('E0005', 'Sofía Torres', 'storres@example.com', '999999000'),
('E0006', 'Juan Vargas', 'jvargas@example.com', '998888111'),
('E0007', 'Raquel Guzmán', 'rguzman@example.com', '997777222'),
('E0008', 'Diego Castillo', 'dcastillo@example.com', '996666333'),
('E0009', 'Camila Soto', 'csoto@example.com', '995555444'),
('E0010', 'Pablo Rojas', 'projas@example.com', '994444555');


INSERT INTO Venta (id_venta, fecha, id_cliente, id_empleado) VALUES
('V0001', '2024-11-01 10:00:00', 'CL0001', 'E0001'), 
('V0002', '2024-11-02 11:00:00', 'CL0002', 'E0002'), 
('V0003', '2024-11-03 12:30:00', 'CL0003', 'E0003'), 
('V0004', '2024-11-04 13:15:00', 'CL0004', 'E0004'), 
('V0005', '2024-11-05 09:45:00', 'CL0005', 'E0005'), 
('V0006', '2024-11-06 08:00:00', 'CL0006', 'E0006'), 
('V0007', '2024-11-07 15:30:00', 'CL0007', 'E0007'), 
('V0008', '2024-11-08 16:45:00', 'CL0008', 'E0008'), 
('V0009', '2024-11-09 17:15:00', 'CL0009', 'E0009'), 
('V0010', '2024-11-10 18:30:00', 'CL0010', 'E0010'); 



INSERT INTO Detalle_Venta (id_venta, id_producto, cantidad, precio_unitario) VALUES
('V0001', 'PRD001', 3, 1.50), 
('V0001', 'PRD002', 4, 0.30), 
('V0001', 'PRD003', 5, 0.10), 

('V0002', 'PRD004', 8, 2.00),  
('V0002', 'PRD005', 10, 1.20), 
('V0002', 'PRD006', 5, 1.80),

('V0003', 'PRD007', 20, 0.50), 
('V0003', 'PRD008', 6, 3.00),  
('V0003', 'PRD009', 10, 0.20),

('V0004', 'PRD010', 12, 1.00),
('V0004', 'PRD001', 10, 1.50),  
('V0004', 'PRD002', 15, 0.30),  

('V0005', 'PRD003', 20, 0.10), 
('V0005', 'PRD004', 6, 2.00),  
('V0005', 'PRD005', 10, 1.20),

('V0006', 'PRD006', 8, 1.80),  
('V0006', 'PRD007', 5, 0.50),  
('V0006', 'PRD008', 7, 3.00),

('V0007', 'PRD009', 10, 0.20), 
('V0007', 'PRD010', 12, 1.00),

('V0008', 'PRD001', 15, 1.50), 
('V0008', 'PRD002', 10, 0.30), 
('V0008', 'PRD003', 20, 0.10),

('V0009', 'PRD004', 5, 2.00),  
('V0009', 'PRD005', 8, 1.20),  
('V0009', 'PRD006', 10, 1.80),

('V0010', 'PRD007', 15, 0.50), 
('V0010', 'PRD008', 6, 3.00),  
('V0010', 'PRD009', 12, 0.20);



-- ======================================================== Listar ========================================================

-- Procedimiento almacenado para listar todas las Marcas
go
CREATE PROCEDURE ListarMarcas
AS
BEGIN
    SELECT * FROM Marca;
END;
GO

-- Procedimiento almacenado para listar todos los Proveedores
CREATE PROCEDURE ListarProveedores
AS
BEGIN
    SELECT * FROM Proveedores;
END;
GO

CREATE PROCEDURE ListarProveedoresDatos
AS
BEGIN
    SELECT id_proveedor, nombre
    FROM Proveedores;
END;
GO


-- Procedimiento almacenado para listar todas las Categorías
CREATE PROCEDURE ListarCategorias
AS
BEGIN
    SELECT * FROM Categoria;
END;
GO

-- Procedimiento almacenado para listar todos los Clientes
CREATE PROCEDURE ListarClientes
AS
BEGIN
    SELECT * FROM Cliente;
END;
GO

-- Procedimiento almacenado para listar todos los Administradores
CREATE PROCEDURE ListarEmpleado
AS
BEGIN
    SELECT * FROM Empleado;
END;
GO

CREATE PROCEDURE ListarAdministradores
AS
BEGIN
    SELECT * FROM Administrador;
END;
GO

-- Procedimiento almacenado para listar todos los productos con marca, categoría y proveedor
CREATE PROCEDURE ListarProductos
AS
BEGIN
    SELECT 
        p.id_producto,
        p.nombre ,
        p.descripcion,
        p.precio,
        p.stock,
        m.nombre AS id_marca,
        pr.nombre AS id_proveedor,
        c.nombre AS id_categoria
    FROM 
        Productos p
    LEFT JOIN Marca m ON p.id_marca = m.id_marca
    LEFT JOIN Proveedores pr ON p.id_proveedor = pr.id_proveedor
    LEFT JOIN Categoria c ON p.id_categoria = c.id_categoria;
END;
GO

-- Procedimiento almacenado para listar todas las Ventas
CREATE PROCEDURE ListarVentas
AS
BEGIN
    SELECT 
        v.id_venta,
        v.fecha,
        c.nombre AS id_cliente,
        a.nombre AS id_empleado
    FROM Venta v
    LEFT JOIN Cliente c ON v.id_cliente = c.id_cliente
    LEFT JOIN Empleado a ON v.id_empleado = a.id_empleado;
END;
GO

-- Procedimiento almacenado para listar todos los Detalles de Ventas
CREATE PROCEDURE ListarDetalleVentas
AS
BEGIN
    SELECT 
        dv.id_detalle,
        v.id_venta,
        v.fecha AS fecha_venta,
        p.nombre AS nombre_producto,
        dv.cantidad,
        dv.precio_unitario
    FROM Detalle_Venta dv
    LEFT JOIN Venta v ON dv.id_venta = v.id_venta
    LEFT JOIN Productos p ON dv.id_producto = p.id_producto;
END;
GO


-- ======================================================== CANTIDAD  ========================================================
CREATE PROCEDURE ObtenerCantidadProveedores
AS
BEGIN
    SELECT COUNT(*) AS CantidadProveedores
    FROM Proveedores;
END;
GO

CREATE PROCEDURE ObtenerCantidadProductos
AS
BEGIN
    SELECT COUNT(*) AS CantidadProductos
    FROM Productos;
END;
GO

CREATE PROCEDURE ObtenerCantidadClientes
AS
BEGIN
    SELECT COUNT(*) AS CantidadClientes
    FROM Cliente;
END;
GO

CREATE PROCEDURE ObtenerCantidadesTotales
AS
BEGIN
    SELECT 
        (SELECT COUNT(*) FROM Proveedores) AS CantidadProveedores,
        (SELECT COUNT(*) FROM Productos) AS CantidadProductos,
        (SELECT COUNT(*) FROM Cliente) AS CantidadClientes;
END;
GO


-- ======================================================== CONSULTAR  ========================================================
CREATE PROCEDURE consultarCliente
    @id_cliente NVARCHAR(6)
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Cliente WHERE id_cliente = @id_cliente)
    BEGIN
        SELECT 
            id_cliente,
            nombre,
            email,
            telefono,
            direccion
        FROM 
            Cliente
        WHERE 
            id_cliente = @id_cliente;
    END
    ELSE
    BEGIN
        PRINT 'El cliente con el ID proporcionado no existe.';
    END
END;
GO

CREATE PROCEDURE consultarProveedor
    @id_proveedor NVARCHAR(5)
AS
BEGIN
    SELECT 
        id_proveedor,
        nombre,
        telefono,
        direccion
    FROM 
        Proveedores
    WHERE 
        id_proveedor = @id_proveedor;
END;
GO

CREATE PROCEDURE consultarCategoria
    @id_categoria NVARCHAR(5)  -- Parámetro para el identificador de la categoría
AS
BEGIN
    SET NOCOUNT ON;

    -- Consulta para obtener la categoría por id_categoria
    SELECT id_categoria, nombre
    FROM Categoria
    WHERE id_categoria = @id_categoria;

    -- Si no se encuentra la categoría, se puede retornar un mensaje o resultado vacío.
    IF NOT EXISTS (SELECT 1 FROM Categoria WHERE id_categoria = @id_categoria)
    BEGIN
        PRINT 'La categoría no existe.';
    END
END;
GO

CREATE PROCEDURE consultarEmpleado
    @id_empleado NVARCHAR(5)
AS
BEGIN
    -- Selecciona el empleado que coincide con el id proporcionado
    SELECT id_empleado, nombre, email, telefono
    FROM Empleado
    WHERE id_empleado = @id_empleado;

    -- Opcional: Mensaje para indicar si no hay registros (solo se verá en el servidor SQL)
    IF @@ROWCOUNT = 0
    BEGIN
        PRINT 'No se encontró un empleado con el ID proporcionado.';
    END
END;
GO

CREATE PROCEDURE consultarProducto
    @id_producto NVARCHAR(6)
AS
BEGIN
    -- Verifica si el producto existe
    IF EXISTS (SELECT 1 FROM Productos WHERE id_producto = @id_producto)
    BEGIN
        -- Selecciona los detalles del producto
        SELECT 
            id_producto, 
            nombre, 
            descripcion, 
            precio, 
            stock, 
            id_marca, 
            id_proveedor, 
            id_categoria
        FROM 
            Productos
        WHERE 
            id_producto = @id_producto;
    END
    ELSE
    BEGIN
        -- Devuelve un mensaje si no se encuentra el producto
        PRINT 'No se encontró un producto con el ID proporcionado.';
    END
END;
GO


CREATE PROCEDURE consultarMarca
    @id_marca NVARCHAR(5)
AS
BEGIN
    SELECT * 
    FROM Marca
    WHERE id_marca = @id_marca;
END;
GO


-- ======================================================== ACTUALIZAR  ========================================================
CREATE PROCEDURE actualizarCategoria
    @id_categoria NVARCHAR(5),        -- Identificador de la categoría a editar
    @nuevo_nombre NVARCHAR(100) -- Nuevo nombre de la categoría
AS
BEGIN
    SET NOCOUNT ON;

    -- Verificar si la categoría existe antes de actualizar
    IF EXISTS (SELECT 1 FROM Categoria WHERE id_categoria = @id_categoria)
    BEGIN
        -- Actualizar el nombre de la categoría
        UPDATE Categoria
        SET nombre = @nuevo_nombre
        WHERE id_categoria = @id_categoria;

        PRINT 'Categoría actualizada correctamente.';
    END
    ELSE
    BEGIN
        PRINT 'La categoría no existe.';
    END
END;
GO



-- ======================================================== SELECT CSHTML  ========================================================
CREATE PROCEDURE ListarDatosRelacionados
AS
BEGIN
    SELECT 
        pr.id_proveedor AS id_proveedor,
        pr.nombre AS proveedor,
        c.id_categoria AS id_Categoria,
        c.nombre AS categoria,
        m.id_marca AS id_Marca,
        m.nombre AS marca
    FROM 
        Proveedores pr
    CROSS JOIN 
        Categoria c
    CROSS JOIN 
        Marca m;
END;
GO
    
CREATE PROCEDURE guardarProducto
    @id_producto NVARCHAR(50),
    @nombre NVARCHAR(100),
    @descripcion NVARCHAR(255),
    @precio DECIMAL(10, 2),
    @stock INT,
    @id_marca NVARCHAR(50),
    @id_proveedor NVARCHAR(50),
    @id_categoria NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    -- Verificar si algún campo esencial es NULL
    IF @id_producto IS NULL OR @nombre IS NULL OR @descripcion IS NULL OR @precio IS NULL OR @stock IS NULL OR @id_marca IS NULL OR @id_proveedor IS NULL OR @id_categoria IS NULL
    BEGIN
        RAISERROR ('No se pueden insertar valores NULL en los campos obligatorios.', 16, 1);
        RETURN;  -- Detener el procedimiento si hay NULLs
    END

    -- Verificar si el producto ya existe
    IF EXISTS (SELECT 1 FROM Productos WHERE id_producto = @id_producto)
    BEGIN
        -- Actualizar el producto existente
        UPDATE Productos
        SET
            nombre = @nombre,
            descripcion = @descripcion,
            precio = @precio,
            stock = @stock,
            id_marca = @id_marca,
            id_proveedor = @id_proveedor,
            id_categoria = @id_categoria
        WHERE id_producto = @id_producto;
    END
    ELSE
    BEGIN
        -- Insertar un nuevo producto
        INSERT INTO Productos (id_producto, nombre, descripcion, precio, stock, id_marca, id_proveedor, id_categoria)
        VALUES (@id_producto, @nombre, @descripcion, @precio, @stock, @id_marca, @id_proveedor, @id_categoria);
    END
END;
GO

CREATE PROCEDURE ObtenerDetalleVentaMasAlto
AS
BEGIN
    SELECT TOP 1 id_detalle
    FROM Detalle_Venta
    ORDER BY id_detalle DESC;
END;
GO

CREATE PROCEDURE ObtenerDetallesPorVenta
    @id_venta NVARCHAR(5)
AS
BEGIN
    SELECT 
        dv.id_detalle,
        dv.id_venta,
        p.nombre AS id_producto,
        dv.cantidad,
        dv.precio_unitario
    FROM Detalle_Venta dv
    INNER JOIN Productos p ON dv.id_producto = p.id_producto  -- Unimos con la tabla Productos
    WHERE dv.id_venta = @id_venta;
END;
