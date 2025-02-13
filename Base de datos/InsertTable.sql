-- Insert sample data
INSERT INTO tblusuarios (nombres, apellidos, fechanacimiento, direccion, password, telefono, email, fechacreacion)
VALUES 
    ('Juan Carlos', 'Pérez González', '1990-05-15', 'Av. Principal 123, Ciudad', 'hash123password456', '+34612345678', 'juan.perez@email.com', CURRENT_date),
    ('María Elena', 'García Ruiz', '1988-09-22', 'Calle Nueva 45, Villa', 'securepass789', '+34623456789', 'maria.garcia@email.com', CURRENT_date),
    ('Roberto', 'Martínez López', '1995-03-10', 'Plaza Mayor 67, Pueblo', 'userpass321', '+34634567890', 'roberto.martinez@email.com', CURRENT_date),
    ('Ana Isabel', 'Sánchez Vega', '1992-11-30', 'Carrera 89, Barrio Alto', 'mypass456!', '+34645678901', 'ana.sanchez@email.com', CURRENT_date),
    ('Luis Miguel', 'Rodríguez Torres', '1985-07-25', 'Paseo Central 234, Centro', 'securePwd789!', '+34656789012', 'luis.rodriguez@email.com', CURRENT_date);

