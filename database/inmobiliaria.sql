-- phpMyAdmin SQL Dump
-- version 5.1.0
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 07-11-2025 a las 17:12:23
-- Versión del servidor: 10.4.19-MariaDB
-- Versión de PHP: 8.0.6

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `inmobiliaria`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `contratos`
--

CREATE TABLE `contratos` (
  `IdContrato` int(11) NOT NULL,
  `FechaInicio` datetime(6) NOT NULL,
  `FechaFinalizacion` datetime(6) NOT NULL,
  `MontoAlquiler` double NOT NULL,
  `Estado` tinyint(1) NOT NULL,
  `IdInquilino` int(11) NOT NULL,
  `IdInmueble` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `contratos`
--

INSERT INTO `contratos` (`IdContrato`, `FechaInicio`, `FechaFinalizacion`, `MontoAlquiler`, `Estado`, `IdInquilino`, `IdInmueble`) VALUES
(2, '2025-11-01 00:00:00.000000', '2026-11-01 00:00:00.000000', 150000, 1, 1, 5);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inmuebles`
--

CREATE TABLE `inmuebles` (
  `IdInmueble` int(11) NOT NULL,
  `Direccion` longtext NOT NULL,
  `Uso` longtext NOT NULL,
  `Tipo` longtext NOT NULL,
  `Ambientes` int(11) NOT NULL,
  `Superficie` double NOT NULL,
  `Latitud` double NOT NULL,
  `Longitud` double NOT NULL,
  `Valor` double NOT NULL,
  `Imagen` longtext NOT NULL,
  `Disponible` tinyint(1) NOT NULL,
  `IdPropietario` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `inmuebles`
--

INSERT INTO `inmuebles` (`IdInmueble`, `Direccion`, `Uso`, `Tipo`, `Ambientes`, `Superficie`, `Latitud`, `Longitud`, `Valor`, `Imagen`, `Disponible`, `IdPropietario`) VALUES
(1, 'Libertad 320', 'Residencial', 'Departamento', 3, 85.5, -31.4201, -64.1888, 75000, 'https://via.placeholder.com/150', 0, 1),
(2, 'Berrondo 66', 'Residencial', 'Departamento', 2, 70.5, -31.4201, -64.1888, 60000, '/images/51945048-6b3c-4935-9338-821082eb7797.jpg', 0, 1),
(3, 'Av Lafinur 43', 'Residencial', 'Local', 2, 70.5, -31.4201, -64.1888, 60000, '/images/87aab3e3-8f52-43b4-9fe3-a4a3b67503f2.jpg', 0, 1),
(4, 'Rivadavia 1200', 'Residencial', 'Casa', 2, 70.5, -31.4201, -64.1888, 60000, '/images/d00c1175-573a-4800-9f42-b644066e2b7d.jpg', 0, 1),
(5, 'Av Buenos Aires 32, Piso 2', 'Residencial', 'Local', 3, 70.5, -31.4201, -64.1888, 60000, '/images/0f9dc204-9de9-4094-ae33-7f0c43f306da.jpg', 0, 1),
(6, 'España 35', 'Residencial', 'Departamento', 2, 70.5, -31.4201, -64.1888, 60000, '/images/03adfca6-630c-4ec1-b42f-f03587ea0d66.jpg', 0, 1),
(7, 'Chiozza 1232', 'Residencial', 'Departamento', 2, 70.5, -31.4201, -64.1888, 60000, '/images/c3b2c7dd-7c5d-47ed-8697-118a0bb3da8e.jpg', 0, 1),
(8, 'Sarmiento 2892', 'Residencial', 'Departamento', 4, 70.5, -31.4201, -64.1888, 60000, '/images/daba7048-47a6-4aa5-aaa1-c8f0537e7467.jpg', 0, 1),
(9, 'Caferatta 14 este', 'Residencial', 'Departamento', 2, 70.5, -31.4201, -64.1888, 60000, '/images/3179ed06-fbaa-488c-acf0-6edb735948c4.jpg', 0, 1),
(11, 'Chiozza 1223', 'Temporal', 'Departamento', 2, 70.5, -31.4201, -64.1888, 60000, '/images/54aa5bc5-5e09-4fb9-8bcf-99820c6b44d7.jpg', 0, 1),
(12, 'Chiozza 1223', 'Temporal', 'Departamento', 2, 70.5, -31.4201, -64.1888, 60000, '/images/d0e02c65-da82-4d43-95b5-8f39a537cf1b.jpg', 0, 1),
(13, 'Chiozza 1223', 'Temporal', 'Departamento', 2, 70.5, -31.4201, -64.1888, 60000, '/images/7060faba-a42d-4d2c-849a-e446406fdfde.jpg', 0, 1),
(14, 'Chiozza 1223', 'Temporal', 'Departamento', 2, 70.5, -31.4201, -64.1888, 60000, '/images/e4eb03da-70a4-4e0d-9bd3-86a2ac487673.jpg', 0, 1),
(15, 'Mitre 123', 'Temporal', 'Departamento', 3, 70.5, -31.4201, -64.1888, 60000, '/images/de23696a-8ddb-4b46-9697-a6d009a44a55.jpg', 0, 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inquilinos`
--

CREATE TABLE `inquilinos` (
  `IdInquilino` int(11) NOT NULL,
  `Nombre` longtext NOT NULL,
  `Apellido` longtext NOT NULL,
  `Dni` longtext NOT NULL,
  `Telefono` longtext NOT NULL,
  `Email` longtext NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `inquilinos`
--

INSERT INTO `inquilinos` (`IdInquilino`, `Nombre`, `Apellido`, `Dni`, `Telefono`, `Email`) VALUES
(1, 'Carlos', 'Pérez', '30123456', '3515555555', 'carlos@example.com');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `pagos`
--

CREATE TABLE `pagos` (
  `IdPago` int(11) NOT NULL,
  `FechaPago` datetime(6) NOT NULL,
  `Monto` double NOT NULL,
  `Detalle` longtext NOT NULL,
  `Estado` tinyint(1) NOT NULL,
  `IdContrato` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `pagos`
--

INSERT INTO `pagos` (`IdPago`, `FechaPago`, `Monto`, `Detalle`, `Estado`, `IdContrato`) VALUES
(1, '2025-11-05 00:00:00.000000', 150000, 'Pago inicial de alquiler', 1, 2),
(2, '2025-12-05 00:00:00.000000', 150000, 'Pago mensual correspondiente a diciembre', 1, 2),
(3, '2026-01-05 00:00:00.000000', 150000, 'Pago mensual correspondiente a enero', 1, 2);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `propietarios`
--

CREATE TABLE `propietarios` (
  `IdPropietario` int(11) NOT NULL,
  `Nombre` longtext NOT NULL,
  `Apellido` longtext NOT NULL,
  `Dni` longtext NOT NULL,
  `Email` longtext NOT NULL,
  `Clave` longtext NOT NULL,
  `Telefono` longtext NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `propietarios`
--

INSERT INTO `propietarios` (`IdPropietario`, `Nombre`, `Apellido`, `Dni`, `Email`, `Clave`, `Telefono`) VALUES
(1, 'Luis Javier', 'Mercado', '28789404', 'luis@example.com', '$2a$12$7gwR1z6mYqCBNdOcs0ETRuk22LkvNcNlqpsj5NfJG66/2yZzk2oey', '2664642996');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `__efmigrationshistory`
--

CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `__efmigrationshistory`
--

INSERT INTO `__efmigrationshistory` (`MigrationId`, `ProductVersion`) VALUES
('20251104133817_InitialCreate', '9.0.10');

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `contratos`
--
ALTER TABLE `contratos`
  ADD PRIMARY KEY (`IdContrato`),
  ADD KEY `IX_Contratos_IdInmueble` (`IdInmueble`),
  ADD KEY `IX_Contratos_IdInquilino` (`IdInquilino`);

--
-- Indices de la tabla `inmuebles`
--
ALTER TABLE `inmuebles`
  ADD PRIMARY KEY (`IdInmueble`),
  ADD KEY `IX_Inmuebles_IdPropietario` (`IdPropietario`);

--
-- Indices de la tabla `inquilinos`
--
ALTER TABLE `inquilinos`
  ADD PRIMARY KEY (`IdInquilino`);

--
-- Indices de la tabla `pagos`
--
ALTER TABLE `pagos`
  ADD PRIMARY KEY (`IdPago`),
  ADD KEY `IX_Pagos_IdContrato` (`IdContrato`);

--
-- Indices de la tabla `propietarios`
--
ALTER TABLE `propietarios`
  ADD PRIMARY KEY (`IdPropietario`);

--
-- Indices de la tabla `__efmigrationshistory`
--
ALTER TABLE `__efmigrationshistory`
  ADD PRIMARY KEY (`MigrationId`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `contratos`
--
ALTER TABLE `contratos`
  MODIFY `IdContrato` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT de la tabla `inmuebles`
--
ALTER TABLE `inmuebles`
  MODIFY `IdInmueble` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=16;

--
-- AUTO_INCREMENT de la tabla `inquilinos`
--
ALTER TABLE `inquilinos`
  MODIFY `IdInquilino` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT de la tabla `pagos`
--
ALTER TABLE `pagos`
  MODIFY `IdPago` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT de la tabla `propietarios`
--
ALTER TABLE `propietarios`
  MODIFY `IdPropietario` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `contratos`
--
ALTER TABLE `contratos`
  ADD CONSTRAINT `FK_Contratos_Inmuebles_IdInmueble` FOREIGN KEY (`IdInmueble`) REFERENCES `inmuebles` (`IdInmueble`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_Contratos_Inquilinos_IdInquilino` FOREIGN KEY (`IdInquilino`) REFERENCES `inquilinos` (`IdInquilino`) ON DELETE CASCADE;

--
-- Filtros para la tabla `inmuebles`
--
ALTER TABLE `inmuebles`
  ADD CONSTRAINT `FK_Inmuebles_Propietarios_IdPropietario` FOREIGN KEY (`IdPropietario`) REFERENCES `propietarios` (`IdPropietario`) ON DELETE CASCADE;

--
-- Filtros para la tabla `pagos`
--
ALTER TABLE `pagos`
  ADD CONSTRAINT `FK_Pagos_Contratos_IdContrato` FOREIGN KEY (`IdContrato`) REFERENCES `contratos` (`IdContrato`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
