
CREATE TABLE IF NOT EXISTS public.products (
	id uuid NOT NULL,
	description text NULL,
	"name" text NULL,
	price numeric NOT NULL,
	quantity int4 NOT NULL,
	is_available bool NOT NULL,
	created timestamptz NOT NULL,
	created_by text NULL,
	last_modified timestamptz NOT NULL,
	last_modified_by text NULL,
	CONSTRAINT "PK_products" PRIMARY KEY (id)
);


CREATE TABLE IF NOT EXISTS public.clients (
	id uuid NOT NULL,
	"name" text NOT NULL,
	last_name text NOT NULL,
	is_active bool NOT NULL,
	phone text NOT NULL,
	debt numeric NOT NULL,
	credit numeric NOT NULL,
	created timestamptz NOT NULL,
	created_by text NULL,
	last_modified timestamptz NOT NULL,
	last_modified_by text NULL,
	CONSTRAINT "PK_clients" PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS public.sales_history (
	id uuid NOT NULL,
	"date" timestamptz NOT NULL,
	quantity int4 NOT NULL,
	price_per_unit numeric NOT NULL,
	total_price numeric NOT NULL,
	product_id uuid NOT NULL,
	created timestamptz NOT NULL,
	created_by text NULL,
	last_modified timestamptz NOT NULL,
	last_modified_by text NULL,
	client_name text NOT NULL DEFAULT ''::text,
	payment_type text NOT NULL DEFAULT ''::text,
	CONSTRAINT "PK_sales_history" PRIMARY KEY (id)
);
CREATE INDEX "IX_sales_history_product_fk" ON public.sales_history USING btree (product_id);


DO $$
BEGIN
    IF EXISTS (
        SELECT 1
        FROM pg_constraint
        WHERE conname = 'FK_sales_history_products_product_id' AND conrelid = 'public.sales_history'::regclass
    ) THEN
        ALTER TABLE public.sales_history DROP CONSTRAINT "FK_sales_history_products_product_id";
    END IF;
END $$;


CREATE TABLE IF NOT EXISTS public.orders (
	id uuid NOT NULL,
	status text NOT NULL,
	client_name text NOT NULL,
	created timestamptz NOT NULL,
	created_by text NULL,
	last_modified timestamptz NOT NULL,
	last_modified_by text NULL,
	CONSTRAINT "PK_orders" PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS public.order_products (
	id uuid NOT NULL,
	product_id uuid NOT NULL,
	order_id uuid NOT NULL,
	created timestamptz NOT NULL,
	created_by text NULL,
	last_modified timestamptz NOT NULL,
	last_modified_by text NULL,
	quantity int4 NOT NULL DEFAULT 0,
	CONSTRAINT "PK_order_products" PRIMARY KEY (id)
);

CREATE INDEX IF NOT EXISTS "IX_order_products_order_id" ON public.order_products USING btree (order_id);
CREATE INDEX IF NOT EXISTS "IX_order_products_product_id" ON public.order_products USING btree (product_id);


DO $$
BEGIN
    IF EXISTS (
        SELECT 1
        FROM pg_constraint
        WHERE conname = 'FK_order_products_orders_order_id' AND conrelid = 'public.order_products'::regclass
    ) THEN
        ALTER TABLE public.order_products DROP CONSTRAINT "FK_order_products_orders_order_id";
    END IF;
END $$;


DO $$
BEGIN
    IF EXISTS (
        SELECT 1
        FROM pg_constraint
        WHERE conname = 'FK_order_products_products_product_id' AND conrelid = 'public.order_products'::regclass
    ) THEN
        ALTER TABLE public.order_products DROP CONSTRAINT "FK_order_products_products_product_id";
    END IF;
END $$;