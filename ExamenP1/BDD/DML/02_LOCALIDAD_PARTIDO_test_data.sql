-- =============================================
-- Datos de prueba: LOCALIDAD_PARTIDO (20 registros)
-- =============================================

SET IDENTITY_INSERT LOCALIDAD_PARTIDO ON;

INSERT INTO LOCALIDAD_PARTIDO (Id, CodigoLocalidad, Disponibilidad, Precio, PartidoCodigo) VALUES
-- Partido 1: Barcelona FC vs Real Madrid (4 localidades)
(1,  'TRI-N-001', 120, 150.00, 1),
(2,  'TRI-S-002', 100, 150.00, 1),
(3,  'PREF-003',  250,  90.00, 1),
(4,  'GEN-004',   500,  50.00, 1),

-- Partido 2: Atlético Madrid vs Sevilla FC (3 localidades)
(5,  'TRI-N-005',  80, 120.00, 2),
(6,  'PREF-006',  200,  75.00, 2),
(7,  'GEN-007',   400,  40.00, 2),

-- Partido 3: Valencia CF vs Real Betis (3 localidades)
(8,  'TRI-E-008',  90, 110.00, 3),
(9,  'PREF-009',  180,  70.00, 3),
(10, 'GEN-010',   350,  35.00, 3),

-- Partido 4: Athletic Club vs Real Sociedad (4 localidades)
(11, 'TRI-N-011', 100, 130.00, 4),
(12, 'TRI-S-012', 100, 130.00, 4),
(13, 'PREF-013',  220,  80.00, 4),
(14, 'GEN-014',   450,  45.00, 4),

-- Partido 5: Villarreal CF vs RC Celta (3 localidades)
(15, 'TRI-C-015',  70, 100.00, 5),
(16, 'PREF-016',  160,  60.00, 5),
(17, 'GEN-017',   300,  30.00, 5),

-- Partido 6: Getafe CF vs CA Osasuna (3 localidades)
(18, 'TRI-C-018',  60,  85.00, 6),
(19, 'PREF-019',  150,  55.00, 6),
(20, 'GEN-020',   280,  25.00, 6);

SET IDENTITY_INSERT LOCALIDAD_PARTIDO OFF;
