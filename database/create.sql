CREATE TABLE IF NOT EXISTS "Tasks" (
    "Id" SERIAL PRIMARY KEY,
    "Title" VARCHAR(255) NOT NULL,
    "Description" TEXT,
    "Priority" VARCHAR(20) DEFAULT 'normal',
    "Status" VARCHAR(30) DEFAULT 'not started',
    "CreatedAt" TIMESTAMP NOT NULL DEFAULT NOW()
);