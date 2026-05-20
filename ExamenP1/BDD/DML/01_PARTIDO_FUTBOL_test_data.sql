-- =============================================
-- Datos de prueba: PARTIDO_FUTBOL (6 registros)
-- =============================================

SET IDENTITY_INSERT PARTIDO_FUTBOL ON;

INSERT INTO PARTIDO_FUTBOL (Codigo, EquipoLocal, EquipoVisita, Fecha, Lugar) VALUES
(1, 'LDU Quito',              'Barcelona SC',          '2026-06-15 20:00:00', 'Estadio Rodrigo Paz Delgado, Quito'),
(2, 'Independiente del Valle','Emelec',                '2026-06-16 18:30:00', 'Estadio Banco Guayaquil, Sangolquí'),
(3, 'El Nacional',            'Aucas',                 '2026-06-17 21:00:00', 'Estadio Olímpico Atahualpa, Quito'),
(4, 'Deportivo Cuenca',       'Universidad Católica',  '2026-06-20 19:00:00', 'Estadio Alejandro Serrano Aguilar, Cuenca'),
(5, 'Delfín SC',              'Macará',                '2026-06-21 17:00:00', 'Estadio Jocay, Manta'),
(6, 'Técnico Universitario',  'Mushuc Runa',           '2026-06-22 16:15:00', 'Estadio Bellavista, Ambato');

SET IDENTITY_INSERT PARTIDO_FUTBOL OFF;

-- =============================================
-- Datos de prueba: LOCALIDAD_PARTIDO (20 registros)
-- =============================================

SET IDENTITY_INSERT LOCALIDAD_PARTIDO ON;

INSERT INTO LOCALIDAD_PARTIDO (Id, CodigoLocalidad, Disponibilidad, Precio, PartidoCodigo) VALUES
-- Partido 1: LDU Quito vs Barcelona SC (4 localidades)
(1,  'TRIBUNA NORTE', 120, 150.00, 1),
(2,  'TRIBUNA SUR',   100, 150.00, 1),
(3,  'PALCO',         250,  90.00, 1),
(4,  'GENERAL',       500,  50.00, 1),

-- Partido 2: Independiente del Valle vs Emelec (3 localidades)
(5,  'TRIBUNA NORTE',  80, 120.00, 2),
(6,  'PALCO',         200,  75.00, 2),
(7,  'GENERAL',       400,  40.00, 2),

-- Partido 3: El Nacional vs Aucas (3 localidades)
(8,  'TRIBUNA',        90, 110.00, 3),
(9,  'PALCO',         180,  70.00, 3),
(10, 'GENERAL',       350,  35.00, 3),

-- Partido 4: Deportivo Cuenca vs Universidad Católica (4 localidades)
(11, 'TRIBUNA NORTE', 100, 130.00, 4),
(12, 'TRIBUNA SUR',   100, 130.00, 4),
(13, 'PREFERENCIA',   220,  80.00, 4),
(14, 'GENERAL',       450,  45.00, 4),

-- Partido 5: Delfín SC vs Macará (3 localidades)
(15, 'TRIBUNA',        70, 100.00, 5),
(16, 'PALCO',         160,  60.00, 5),
(17, 'GENERAL',       300,  30.00, 5),

-- Partido 6: Técnico Universitario vs Mushuc Runa (3 localidades)
(18, 'TRIBUNA',        60,  85.00, 6),
(19, 'PALCO',         150,  55.00, 6),
(20, 'GENERAL',       280,  25.00, 6);

SET IDENTITY_INSERT LOCALIDAD_PARTIDO OFF;