CREATE TABLE IF NOT EXISTS employee(
                                       id SERIAL PRIMARY KEY,
                                       first_name VARCHAR(30) NOT NULL,
                                       last_name VARCHAR(30) NOT NULL,
                                       middle_name VARCHAR(30)
);

CREATE TABLE IF NOT EXISTS client(
                                     id SERIAL PRIMARY KEY,
                                     name VARCHAR(30) NOT NULL
);

CREATE TABLE IF NOT EXISTS order_status(
                                           id SERIAL PRIMARY KEY,
                                           title VARCHAR(30) NOT NULL,
                                           allowed_change_order_data boolean NOT NULL,
                                           require_comment boolean NOT NULL
);

CREATE TABLE IF NOT EXISTS "order"(
                                      id SERIAL PRIMARY KEY,
                                      title VARCHAR(60) NOT NULL,
                                      delivery_date timestamp with time zone NOT NULL,
                                      comment VARCHAR(60),
                                      delivery_location VARCHAR(60) NOT NULL,
                                      client_id INTEGER REFERENCES client(id) NOT NULL,
                                      status_id INTEGER REFERENCES order_status(id) NOT NULL
);

CREATE TABLE IF NOT EXISTS appointed_employee(
                                                 employee_id INTEGER REFERENCES employee(id),
                                                 order_id INTEGER REFERENCES "order"(id),
                                                 CONSTRAINT appointed_employee_pkey PRIMARY KEY (order_id, employee_id)
);

INSERT INTO order_status (id, title, allowed_change_order_data, require_comment)
VALUES (1, 'Новая', true, false),
       (2, 'Передано на выполнение', false, false),
       (3, 'Выполнено', false, false),
       (4, 'Отменена', false, true);