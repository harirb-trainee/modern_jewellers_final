-- SQL Script to update Modern Jewellers database schema
-- Run this in your PostgreSQL tool (e.g., pgAdmin or psql)

-- 1. Create stone_type table
CREATE TABLE IF NOT EXISTS public.stone_type (
    stone_type_id SERIAL PRIMARY KEY,
    name CHARACTER VARYING(100) UNIQUE,
    description TEXT,
    status BOOLEAN DEFAULT TRUE
);

-- 2. Create fitting_type table
CREATE TABLE IF NOT EXISTS public.fitting_type (
    fitting_type_id SERIAL PRIMARY KEY,
    name CHARACTER VARYING(100) UNIQUE,
    description TEXT,
    status BOOLEAN DEFAULT TRUE
);

-- 3. Create pattern table
CREATE TABLE IF NOT EXISTS public.pattern (
    pattern_id SERIAL PRIMARY KEY,
    name CHARACTER VARYING(100) UNIQUE,
    description TEXT,
    status BOOLEAN DEFAULT TRUE
);

-- 4. Alter item table to add new columns
DO $$ 
BEGIN 
    IF NOT EXISTS (SELECT 1 FROM information_schema.columns WHERE table_name='item' AND column_name='stone_id') THEN
        ALTER TABLE public.item ADD COLUMN stone_id INTEGER;
    END IF;

    IF NOT EXISTS (SELECT 1 FROM information_schema.columns WHERE table_name='item' AND column_name='fitting_id') THEN
        ALTER TABLE public.item ADD COLUMN fitting_id INTEGER;
    END IF;

    IF NOT EXISTS (SELECT 1 FROM information_schema.columns WHERE table_name='item' AND column_name='pattern_id') THEN
        ALTER TABLE public.item ADD COLUMN pattern_id INTEGER;
    END IF;
END $$;

-- 5. Add foreign key constraints
DO $$ 
BEGIN 
    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'stone_fkey') THEN
        ALTER TABLE public.item ADD CONSTRAINT stone_fkey FOREIGN KEY (stone_id) REFERENCES public.stone_type (stone_type_id);
    END IF;

    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'fitting_fkey') THEN
        ALTER TABLE public.item ADD CONSTRAINT fitting_fkey FOREIGN KEY (fitting_id) REFERENCES public.fitting_type (fitting_type_id);
    END IF;

    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'pattern_fkey') THEN
        ALTER TABLE public.item ADD CONSTRAINT pattern_fkey FOREIGN KEY (pattern_id) REFERENCES public.pattern (pattern_id);
    END IF;
END $$;
