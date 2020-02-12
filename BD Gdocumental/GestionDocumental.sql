Create Table gd_tiposdocumentos
(
idTipo Number Not Null,
NombreTabla Varchar2(200) Not Null,
CampoTabla Varchar2(200),
Descripcion Varchar2(200),
Primary Key(idTipo)
);

Create Table gd_comentarios
(
idComentario Number Not Null,
Fecha Varchar2(200) Not Null,
idTipo Number,
idPersona Number,
Descripcion Varchar2(200) Not Null,
Primary Key(idComentario),
Foreign Key(idTipo) References gd_tiposdocumentos
);
