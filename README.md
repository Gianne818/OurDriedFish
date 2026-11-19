# Our Dried Fish®

A Blazor WebAssembly + Tailwind CSS storefront for authentic Philippine dried fish delicacies, backed by a **Supabase (PostgreSQL + Data API)** database.

## Tech Stack
- **Front End:** Blazor WebAssembly (`.NET 8`) + Tailwind CSS
- **Back End & Database:** Supabase (PostgreSQL + auto-generated REST Data API via `Supabase` C# client)

## Database Schema
- Full table definitions, constraints, indexes, `GRANT`s, and RLS policies live in [`database/schema.sql`](database/schema.sql).
- Migration and seed data (9 dried fish items and 8 customer reviews) live in [`database/seed.sql`](database/seed.sql).

### Tables
1. **`public.dried_fish_items`** — Product catalog with `id`, pricing, stock, category, origin, image, and `deleted_at` (soft delete).
2. **`public.product_reviews`** — Customer reviews linked to `dried_fish_items(id)` via `product_id` (nullable for general site feedback), with `CHECK` constraints for rating (`1–5`) and text lengths.

## Storage Pattern & Why (Soft Delete)
Our `dried_fish_items` table uses the **Soft Delete** pattern via a nullable `deleted_at timestamptz` column instead of permanently deleting rows. When a store manager removes a product in the Gallery, the app sends a `PATCH` request that sets `deleted_at = now()`, while list queries filter `.Where(p => p.DeletedAt == null)` in PostgreSQL so removed items stay hidden from shoppers. We chose soft delete because many dried fish varieties (such as Danggit and Dried Flounder) are seasonal and may return to the catalog later without needing to re-enter their descriptions, preparation tips, and pricing. Keeping the row also preserves historical product references for customer reviews in `product_reviews` and allows accidental deletions to be restored cleanly.

## Fast Database Habits Applied
- **Habit 1 (Indexes):** Indexed `dried_fish_items(name)`, `dried_fish_items(category)`, `dried_fish_items(deleted_at)`, `product_reviews(product_id)`, and `product_reviews(created_at)`.
- **Habit 2 (Filter in the database):** Search, category filters, soft-delete exclusion (`deleted_at is null`), sorting, and `.Range(0, 49)` all run in PostgreSQL before `.Get()`.
- **Habit 3 (One request, not many):** Gallery loads in a single `GET` request, and Reviews computes rating averages and counts from the single fetched list with zero queries inside loops.
- **Double-click protection:** Save/Post buttons are disabled while `POST`/`PATCH` requests are in flight.

## Running Locally
1. Run [`database/schema.sql`](database/schema.sql) (and optionally [`database/seed.sql`](database/seed.sql)) in the Supabase SQL Editor.
2. Put your Supabase Project URL and Publishable key (`sb_publishable_...`) in `OurDriedFish/wwwroot/appsettings.json` (never commit a secret key or database password).
3. Build and run:
   ```bash
   dotnet run --project OurDriedFish
   ```
