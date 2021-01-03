CREATE DATABASE Tutorias;
use Tutorias;

CREATE TABLE usuario(
    codigo int primary key auto_increment,
    password varchar(64)not null
);
ALTER TABLE usuario AUTO_INCREMENT = 10000;

CREATE TABLE carrera(
    codigo int primary key auto_increment,
    nombre varchar(30) not null,
    nivel varchar(20) not null,
    area varchar(20) not null 
);

CREATE TABLE profesor(
    codigo int primary key,
    foreign key (codigo) references usuario(codigo),
    nombre varchar(40) not null,
    apellidos varchar(40) not null,
    email varchar(30) default '',
    telefono varchar(30) default '',
    carrera_adscrito int,
    foreign key (carrera_adscrito) references carrera (codigo)
);

create table periodo(
    codigo int primary key auto_increment,
    inicio date,
    final date
);

create table tutor(
    codigo int primary key,
    foreign key (codigo) references profesor(codigo)
);


CREATE TABLE grupo(
    codigo int primary key auto_increment,
    nombre varchar(30) not null,
    carrera int,
    foreign key (carrera) references carrera (codigo),
    periodo int,
    foreign key (periodo) references periodo (codigo),
    tutor int,
    foreign key (tutor) references tutor (codigo)
);

CREATE TABLE estudiante(
    codigo int primary key,
    foreign key (codigo) references usuario(codigo),
    nombre varchar(40) not null,
    apellidos varchar(40) not null,
    email varchar(30) default '',
    grupo int,
    foreign key (grupo) references grupo(codigo)
);

CREATE TABLE administrador(
    codigo int primary key,
    foreign key (codigo) references usuario(codigo),
    nombre varchar(40) not null,
    email varchar(30) default '',
    apellidos varchar(40) not null
);


create table tutoria(
    codigo int primary key auto_increment,
    tutor int,
    foreign key (tutor) references tutor(codigo),
    fecha date,
    tipo varchar(10),
    periodo int,
    foreign key (periodo) references periodo(codigo)
);

create table grupal(
    codigo int primary key auto_increment,
    tutoria int,
    foreign key (tutoria) references tutoria(codigo),
    grupo int,
    foreign key (grupo) references grupo(codigo)
);


create table plan(
    codigo int primary key auto_increment,
    plan varchar(40),
    tutoria int,
    foreign key (tutoria) references tutoria(codigo)
);

create table informe(
    codigo int primary key auto_increment,
    plan int,
    foreign key (plan) references plan(codigo),
    informe varchar(50)
);

create table actividad(
    codigo int primary key auto_increment,
    fecha date,
    descripcion varchar (200),
    plan int,
    foreign key (plan) references plan(codigo)
);

create table estudiante_tutor(
    codigo int primary key auto_increment,
    estudiante int,
    foreign key (estudiante) references estudiante(codigo),
    profesor int,
    foreign key (profesor) references profesor(codigo)
);

create table individual(
    codigo int primary key auto_increment,
    tutoria int,
    foreign key (tutoria) references tutoria(codigo),
    estudiante int,
    foreign key (estudiante) references estudiante(codigo)
);

create table bitacora(
    codigo int primary key auto_increment,
    individual int,
    foreign key (individual) references individual(codigo),
    objetivo varchar(100),
    meta varchar (100),
    acuerdo varchar (200)
);

create table par(
    codigo int primary key auto_increment,
    tutoria int,
    foreign key (tutoria) references tutoria(codigo),
    estudianteI int,
    foreign key (estudianteI) references estudiante(codigo),
    estudianteR int,
    foreign key (estudianteR) references estudiante(codigo)
);


INSERT INTO `usuario` (`password`) VALUES ('123');
INSERT INTO `administrador` (`codigo`, `nombre`, `apellidos`) VALUES ('10000', 'Emmanuel', 'Gutierrez');

