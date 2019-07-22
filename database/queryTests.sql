insert into route (train_name, departure_date, departure_hour)
	values ('TF027','2019-03-12','14:40');
	
insert into subroute (from_station, to_station, price, travel_duration)
	values ('Warszawa','Poznañ',20.80,'03:02');
	
insert into route_subroute (route_order_number, seats_amount,subroute_id,route_id)
	values (1,120,2,3);

insert into traveller (first_name, last_name, email, login, password)
	values ('Jan','Nowak','jan.nowak@gmail.com', 'jan.nowak', 'password');

insert into ticket (name,price_percentage)
	values ('normalny',100);
	
insert into sale (payment_status, from_station, to_station, route_id, traveller_id)
	values (true,'Warszawa','Poznañ',3,1);

insert into sale_ticket (amount,sale_id,ticket_id)
	values (2,1,1);

