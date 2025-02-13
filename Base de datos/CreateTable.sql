--DROP TABLE IF EXISTS public.tlbusuarios;

CREATE TABLE tblusuarios (
    id SERIAL PRIMARY KEY,
    nombres VARCHAR(100) NOT NULL,
    apellidos VARCHAR(100) NOT NULL,
    fechanacimiento DATE NOT NULL,
    direccion VARCHAR(200) NOT NULL,
    password VARCHAR(120) NOT NULL,
    telefono VARCHAR(20) NOT NULL,
    email VARCHAR(100) NOT NULL,
    fechacreacion date DEFAULT CURRENT_date NOT NULL,
    fechamodificacion date
);