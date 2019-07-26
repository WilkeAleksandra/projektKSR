create table route (
	id serial primary key,
	train_name varchar(10) not null,
	departure_date date not null,
	departure_hour time not null
);

create table subroute (
	id serial primary key,
	from_station varchar(25) not null,
	to_station varchar(25) not null,
	price money not null,
	travel_duration interval not null
);

create table route_subroute (
	id serial primary key,
	route_order_number smallint not null,
	seats_amount smallint not null,
	subroute_id integer references subroute(id) not null,
	route_id integer references route(id) not null
);

create table ticket (
	id serial primary key,
	name varchar(40) not null,
	price_percentage numeric check(price_percentage>=0 and price_percentage<=100) not null
);

create table traveller (
	id serial primary key,
	first_name varchar(25) not null,
	last_name varchar (25) not null,
	email varchar(40) unique not null, 
	login varchar(35) unique not null,
	password varchar(20) not null
);

create table sale (
	id serial primary key,
	payment_status boolean default false,
	sale_date date default current_date,
	from_station varchar(25) not null,
	to_station varchar(25) not null,
	route_id integer references route(id),
	traveller_id integer references traveller(id)
);

create table sale_ticket (
	id serial primary key,
	amount smallint not null,
	sale_id integer references sale(id),
	ticket_id integer references ticket(id)	
);




