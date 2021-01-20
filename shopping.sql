create database shopping

use shopping


create table login
(
log_id int primary key identity(1,1),
log_name nvarchar(50),
log_email nvarchar(50),
log_number nvarchar(50),
log_city nvarchar(50),
log_address nvarchar(50),
log_password nvarchar(50),
emp_img nvarchar(max),
emp_client nvarchar(50),
log_role nvarchar(50),
log_status bit ,
fk_job int foreign key references job(j_id),
)



create table category1
(
cat1_id int primary key identity(1,1),
cat1_name nvarchar(50),
)

create table category2
(
cat2_id int primary key identity(1,1),
cat2_fk_cat1 int foreign key references category1(cat1_id),
cat2_name nvarchar(50),
)

create table product
(
pro_id int primary key identity(1,1),
pro_fk_cat1 int foreign key references category1(cat1_id),
pro_fk_cat2 int foreign key references category2(cat2_id),
pro_title nvarchar(50),
pro_price int,
pro_discrip nvarchar(1000),
pro_img nvarchar(max),
pro_oldprice money,
pro_brand nvarchar(50),
pro_qty int,
pro_img2 nvarchar(max),
pro_img3 nvarchar(max),

)

create table contact
(
con_id int primary key identity,
con_name nvarchar(50),
con_email nvarchar(50),
con_number nvarchar(50),
con_msg nvarchar(200),
)

create table billing
(
bil_id int primary key identity(1,1),
bil_fk_login int foreign key references login(log_id),
bil_firstname nvarchar(50),
bil_lastname nvarchar(50),
bil_country nvarchar(50),
bil_street nvarchar(50),
bil_city nvarchar(50),
bil_postcode nvarchar(50),
bil_number nvarchar(50),
bil_email nvarchar(50),
bill_address nvarchar(500),
bil_date date,
bil_ordernumber bigint,
bil_payment nvarchar
)

create table vacancis
(
v_id int primary key identity,
v_name nvarchar(50),
v_discrip nvarchar(500),
v_last_date date,
v_img nvarchar(max),
)

create table cart
(
cart_id int primary key identity,
cart_qty int,
cart_fk_log int foreign key references login(log_id),
cart_fk_pro int foreign key references product(pro_id),
)

create table job
(
j_id int primary key identity,
j_fk_vacan int foreign key references vacancis(v_id),
j_fullname nvarchar(50),
j_email nvarchar(50),
j_number nvarchar(50),
j_city nvarchar(50),
j_country nvarchar(50),
j_address nvarchar(50),
j_qualification nvarchar(50),
j_experince nvarchar(50),
j_gander nvarchar(50),
j_age date,
)

create table addEmp
(
emp_id int primary key identity,
emp_fK_job int foreign key references job(j_id),
emp_client nvarchar(50),
emp_role nvarchar(50),
emp_password nvarchar(50),
emp_img nvarchar(max),
emp_status bit,
)

create table comment
(
c_id int primary key identity,
c_message nvarchar(100),
c_date date,
c_fk_log  int foreign key references login(log_id),
c_fk_pro int foreign key references product(pro_id)
)


select * from register
select * from login
select * from category1
select * from category2
select * from addEmp
select * from job
select * from cart
select * from vacancis
select * from contact
select * from product
select * from comment
select * from billing
