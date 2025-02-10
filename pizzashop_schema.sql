alter table country 
Add CONSTRAINT country_pk PRIMARY KEY(id);

create table state(
id serial,
name varchar(100),
created_at TIMESTAMP,
modified_at TIMESTAMP,
created_by int,
modified_by int,
 CONSTRAINT state_pk PRIMARY KEY (id),
 CONSTRAINT country_fk FOREIGN key(id) REFERENCES country(id)
);

create table city(
id serial,
name varchar(100),
created_at TIMESTAMP default now(),
modified_at TIMESTAMP,
created_by int,
modified_by int,
 CONSTRAINT city_pk PRIMARY KEY (id),
 CONSTRAINT country_fk FOREIGN key(id) REFERENCES country(id),
 CONSTRAINT state_fk FOREIGN key(id) REFERENCES state(id)
);


--drop table users
create table users (
	id serial primary key,
 	email VARCHAR (50) UNIQUE not null,
	first_name VARCHAR(50) NOT NULL,
	last_name VARCHAR(50) NOT NULL,
	user_name VARCHAR(50) NOT NULL,
	phone INTEGER NOT NULL,
	country_id INTEGER NOT NULL REFERENCES country(id),
	state_id INTEGER NOT NULL REFERENCES state(id),
	city_id INTEGER NOT NULL REFERENCES city(id),
	address VARCHAR(200) NOT NULL,
	zipcode VARCHAR(50) NOT NULL,
	is_deleted BOOLEAN DEFAULT False,
	is_active BOOLEAN DEFAULT True,
	profile_image VARCHAR(100),
	created_by INTEGER references users(id),
	modified_by INTEGER references users(id),
	created_at TIMESTAMP DEFAULT NOW(),
	updated_at TIMESTAMP
);

create table roles(
id serial primary key not null,
name varchar(50) not null,
created_at TIMESTAMP not null default now(),
modidified_at TIMESTAMP,
created_by int not null references users(id) ,
modified_by int references users(id)
);

create table permissions(
id serial primary key,
name varchar(50),
created_at TIMESTAMP not null default now(),
modidified_at TIMESTAMP,
created_by int not null references users(id) ,
modified_by int references users(id)

);

create table role_permissions(
id serial primary key,
can_view boolean default false,
can_edit boolean default false,
can_delete boolean default false,
created_at TIMESTAMP not null default now(),
modidified_at TIMESTAMP,
created_by int not null references users(id) ,
modified_by int references users(id)
);

create table menu_category(
	id serial primary key not null,
	name varchar(50) not null,
	description varchar(50),
	created_at TIMESTAMP not null default now(),
	modidified_at TIMESTAMP,
	created_by int not null references users(id) ,
	modified_by int references users(id),
	is_deleted boolean  default false
);

create table menu_items(
	id serial primary key not null,
	category_id int references menu_category(id) not null,
	name varchar(50) not null,
	type boolean default true,
	rate NUMERIC(18,2) Not null ,
	quantity int default 0,
	unit_id int references units(id),
	is_available boolean default true,
	image text ,
	description varchar(200),
	tax_percentage numeric(18,2) ,
	is_favourite boolean default false,
	short_code varchar(50) ,
	is_default_tax boolean default false,
	created_at TIMESTAMP not null default now(),
	modidified_at TIMESTAMP,
	created_by int not null references users(id) ,
	modified_by int references users(id),
	is_deleted boolean  default false
);

create table units(
	id serial primary key not null,
	name varchar(80) not null,
	short_name varchar(50),
	created_at TIMESTAMP not null default now(),
	modidified_at TIMESTAMP,
	created_by int not null references users(id) ,
	modified_by int references users(id),
	is_deleted boolean  default false
);

create table modifier_group_id(
	id serial primary key not null,
	name varchar(50) not null,
	description varchar(50),
	created_at TIMESTAMP not null default now(),
	modidified_at TIMESTAMP,
	created_by int not null references users(id) ,
	modified_by int references users(id),
	is_deleted boolean  default false
);

create table modifer(
	id serial primary key not null,
	modifier_group_id int references modifier_group_id(id) not null,
	name varchar(50) not null,
	unit_id int references units(id) not null,
	rate numeric(18,2) not null,
	quantity int,
	description varchar(50),
	created_at TIMESTAMP not null default now(),
	modidified_at TIMESTAMP,
	created_by int not null references users(id) ,
	modified_by int references users(id),
	is_deleted boolean  default false
);

create table sections(
	id serial primary key not null,
	name varchar(50) not null,
	description varchar(50),
	created_at TIMESTAMP not null default now(),
	modidified_at TIMESTAMP,
	created_by int not null references users(id) ,
	modified_by int references users(id),
	is_deleted boolean  default false
	);

create table tables(
	id serial primary key not null,
	section_id int references sections(id) not null,
	name varchar(50) not null,
	capacity int,
	is_available boolean default true,
	created_at TIMESTAMP not null default now(),
	modidified_at TIMESTAMP,
	created_by int not null references users(id) ,
	modified_by int references users(id),
	is_deleted boolean  default false
);

create table taxes_and_fees(
	id serial primary key not null,
	name varchar(50) not null,
	--type boolean default true,
	flat_amount Numeric(18,2),
	percentage Numeric(18,2),
	is_active boolean default false,
	is_default boolean default false,
	tax_value DECIMAL(18,2),
	created_at TIMESTAMP not null default now(),
	modidified_at TIMESTAMP,
	created_by int not null references users(id) ,
	modified_by int references users(id),
	is_deleted boolean  default false
);

create table customer(
	id serial primary key not null,
	name varchar(50) not null,
	email varchar(50),
	phone int not null,
	created_at TIMESTAMP not null default now(),
	modidified_at TIMESTAMP,
	created_by int not null references users(id) ,
	modified_by int references users(id)
);

create table orders(
	id serial primary key not null,
	customer_id int references customer(id) not null,
	order_no serial not null,
	order_status int not null check(order_status>=1 and order_status<=4),
	total_amount numeric(18,2),
	tax numeric(18,2),
	sub_total numeric(18,2),
	discount numeric(18,2),
	paid_amount numeric(18,2),
	notes text,
	is_sgst_selected boolean default false,
	created_at TIMESTAMP not null default now(),
	modidified_at TIMESTAMP,
	created_by int not null references users(id) ,
	modified_by int references users(id)
	
);

create table feedback(
	id serial primary key not null,
	food int check(food>=0 and food<=5),
	service int check(food>=0 and food<=5),
	ambience int check(food>=0 and food<=5),
	comments varchar(200),
	order_id int references orders(id) not null,
	avg_rating int check(avg_rating>=0 and avg_rating<=5),
	created_at TIMESTAMP not null default now(),
	modidified_at TIMESTAMP,
	created_by int not null references users(id) ,
	modified_by int references users(id)
		
);

create table waiting_token(
	id serial primary key not null,
	customer_id int not null REFERENCES customer(id),
	no_of_person int not null,
	section_id int not null references sections(id),
	table_id int not null references tables(id),
	is_assigned boolean default false,
	created_at TIMESTAMP not null default now(),
	modidified_at TIMESTAMP,
	created_by int not null references users(id) ,
	modified_by int references users(id),
	is_deleted boolean  default false
	
	);

	ALTER TABLE modifer RENAME TO modifier;

create table invoices(
	id serial primary key not null,
	order_id int references orders(id) not null,
	modifier_id int references modifier(id) not null,
	quantity_of_modifier int,
	rate_of_modifier numeric(18,2),
	total_amount numeric(18,2),
	created_at TIMESTAMP not null default now(),
	modidified_at TIMESTAMP,
	created_by int not null references users(id) ,
	modified_by int references users(id)
	
);

create table payments(
	id serial primary key not null,
	invoice_id int not null references invoices(id),
	amount numeric(18,2),
	payment_method int not null check(payment_method>=1 and payment_method<=4),
	status int not null check(status>=1 and status<=3),
	created_at TIMESTAMP not null default now(),
	modidified_at TIMESTAMP,
	created_by int not null references users(id) ,
	modified_by int references users(id)

);

create table accounts(
	id serial primary key not null,
	email varchar(50) not null unique,
	password varchar(20) not null,
	role_id int not null references roles(id),
	is_first_login boolean default true,
	created_at TIMESTAMP not null default now(),
	modidified_at TIMESTAMP,
	created_by int not null references users(id) ,
	modified_by int references users(id),
	is_deleted boolean  default false
);

create table ordered_items(
	id serial primary key not null,	
	order_id int not null references orders(id),
	menu_item_id int not null references menu_items(id),
	name varchar(100) not null,
	quantity int not null,
	rate numeric(18,2),
	amount numeric(18,2),
	total_modifier_amount numeric(18,2),
	tax numeric(18,2),
	total_amount numeric(18,2),
	instruction text,
	order_status int not null check(order_status>=1 and order_status<=4),
	ready_item_quantity int,
	created_at TIMESTAMP not null default now(),
	modidified_at TIMESTAMP,
	created_by int not null references users(id) ,
	modified_by int references users(id),
	is_deleted boolean  default false
);

ALTER TABLE modifer_group RENAME TO modifier_group;

create table mapping_menu_item_with_modifiers(
	id serial primary key not null,	
	menu_item_id int not null references menu_items(id),
	modifier_group int not null references modifier_group(id),
	min_selection_required int,
	max_selection_allowed int,
	created_at TIMESTAMP not null default now(),
	modidified_at TIMESTAMP,
	created_by int not null references users(id) ,
	modified_by int references users(id),
	is_deleted boolean  default false
	
);

create table ordered_item_modifier_mapping(
	id serial primary key not null,
	order_item_id int not null references ordered_items(id),
	modifier_id int not null references modifier(id),
	quantity_of_modifier int,
	rate_of_modidifier numeric(18,2),
	total_amount numeric(18,2),
	created_at TIMESTAMP not null default now(),
	modidified_at TIMESTAMP,
	created_by int not null references users(id) ,
	modified_by int references users(id),
	is_deleted boolean  default false

	
);

create table order_tax_mapping(
	id serial not null primary key,
	order_id int not null references orders(id),
	tax_id int not null references taxes_and_fees(id),
	tax_value numeric(18,2),
	created_at TIMESTAMP not null default now(),
	modidified_at TIMESTAMP,
	created_by int not null references users(id) ,
	modified_by int references users(id),
	is_deleted boolean  default false
)

create table table_order_mapping(
	id serial primary key not null,
	order_id int not null references orders(id),
	table_id int not null references tables(id),
	no_of_person int not null,
	created_at TIMESTAMP not null default now(),
	modidified_at TIMESTAMP,
	created_by int not null references users(id) ,
	modified_by int references users(id),
	is_deleted boolean  default false
);

