-- =============================================
-- Datos de prueba: PARTIDO_FUTBOL (6 registros)
-- =============================================

SET IDENTITY_INSERT PARTIDO_FUTBOL ON;

INSERT INTO PARTIDO_FUTBOL (Codigo, EquipoLocal, EquipoVisita, Fecha, Lugar) VALUES
(1, 'Barcelona FC',     'Real Madrid',       '2026-06-15 20:00:00', 'Camp Nou, Barcelona'),
(2, 'Atlético Madrid',  'Sevilla FC',        '2026-06-16 18:30:00', 'Metropolitano, Madrid'),
(3, 'Valencia CF',      'Real Betis',        '2026-06-17 21:00:00', 'Mestalla, Valencia'),
(4, 'Athletic Club',    'Real Sociedad',     '2026-06-20 19:00:00', 'San Mamés, Bilbao'),
(5, 'Villarreal CF',    'RC Celta',          '2026-06-21 17:00:00', 'La Cerámica, Villarreal'),
(6, 'Getafe CF',        'CA Osasuna',        '2026-06-22 16:15:00', 'Coliseum, Getafe');

SET IDENTITY_INSERT PARTIDO_FUTBOL OFF;