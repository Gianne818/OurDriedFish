-- ============================================================
-- Run this in Supabase SQL Editor if you already created
-- dried_fish_items and product_reviews earlier.
-- It adds soft-delete/timestamp columns, indexes, GRANTs,
-- RLS policies, and seeds the 9 products + 8 reviews.
-- ============================================================

alter table public.dried_fish_items
  add column if not exists deleted_at timestamptz null,
  add column if not exists created_at timestamptz not null default now();

create index if not exists idx_dried_fish_items_name on public.dried_fish_items (name);
create index if not exists idx_dried_fish_items_category on public.dried_fish_items (category);
create index if not exists idx_dried_fish_items_deleted_at on public.dried_fish_items (deleted_at);
create index if not exists idx_product_reviews_product_id on public.product_reviews (product_id);
create index if not exists idx_product_reviews_created_at on public.product_reviews (created_at desc);

-- Lock 1: GRANT (fixes "permission denied for table ...")
grant select, insert, update, delete on public.dried_fish_items to anon, authenticated;
grant select, insert, update, delete on public.product_reviews to anon, authenticated;
grant usage, select on all sequences in schema public to anon, authenticated;

-- Lock 2: RLS policies for full CRUD (class policies)
alter table public.dried_fish_items enable row level security;
drop policy if exists "Public can read dried fish items" on public.dried_fish_items;
drop policy if exists "read_all" on public.dried_fish_items;
drop policy if exists "insert_all" on public.dried_fish_items;
drop policy if exists "update_all" on public.dried_fish_items;
drop policy if exists "delete_all" on public.dried_fish_items;

create policy "read_all"   on public.dried_fish_items for select to anon, authenticated using (true);
create policy "insert_all" on public.dried_fish_items for insert to anon, authenticated with check (true);
create policy "update_all" on public.dried_fish_items for update to anon, authenticated using (true) with check (true);
create policy "delete_all" on public.dried_fish_items for delete to anon, authenticated using (true);

alter table public.product_reviews enable row level security;
drop policy if exists "Public can read reviews" on public.product_reviews;
drop policy if exists "Public can insert reviews" on public.product_reviews;
drop policy if exists "read_all" on public.product_reviews;
drop policy if exists "insert_all" on public.product_reviews;
drop policy if exists "update_all" on public.product_reviews;
drop policy if exists "delete_all" on public.product_reviews;

create policy "read_all"   on public.product_reviews for select to anon, authenticated using (true);
create policy "insert_all" on public.product_reviews for insert to anon, authenticated with check (true);
create policy "update_all" on public.product_reviews for update to anon, authenticated using (true) with check (true);
create policy "delete_all" on public.product_reviews for delete to anon, authenticated using (true);

-- Seed 9 dried fish products (skips any already present by id)
insert into public.dried_fish_items (
  id, name, local_name, scientific_name, description, weight_grams,
  price_per_kg, stock_quantity, category, image_url, is_seasonal,
  origin, taste_profile, preparation_tips, available_weights_grams
)
values
  (1, 'DANGGIT', 'Bulad Danggit (Rabbitfish)', 'Siganus canaliculatus',
   'Premium Bantayan Island sun-dried rabbitfish. Delicate, crispy when flash-fried, with a savory, distinct sea salt finish.',
   1000, 900.00, 45, 'Rabbitfish & Spinefoot', 'images/products/danggit.png', true,
   'Bantayan Island, Cebu', 'Savory, crisp, light umami, delightfully crunchy fins',
   'Deep fry in medium heat for 45 to 60 seconds until golden brown. Serve with spicy spiced coconut vinegar and garlic rice.',
   '{250, 500, 1000}'),
  (2, 'DRIED SQUID', 'Pusit Sun-Dried', 'Loligo edulis',
   'Tender, hand-cured whole squids harvested from the Tañon Strait. Naturally sun-dried on bamboo mesh for a deep, rich caramelized sweetness.',
   1000, 1100.00, 30, 'Cephalopods', 'images/products/dried-squid.png', true,
   'Bogo City, Cebu', 'Sweet-savory, rich chew, robust umami aroma',
   'Pan fry quickly in a touch of oil for 30 seconds per side. Do not overcook to preserve tender chewiness.',
   '{250, 500, 1000}'),
  (3, 'FISH TOCINO', 'Sweet Cured Fish Fillet', 'Oreochromis niloticus / Chanos chanos',
   'Artisanal sweet-cured, marinated boneless fish fillets air-dried to lock in natural sugars and sea minerals. A vibrant breakfast favorite.',
   1000, 800.00, 25, 'Cured & Marinated', 'images/products/fish-tocino.png', false,
   'Hagnaya, Cebu', 'Sweet glazed exterior, tender flaky meat, subtle caramelized garlic notes',
   'Simmer in 2 tablespoons of water until dry, then lightly fry in its own natural caramel glaze.',
   '{250, 500, 1000}'),
  (4, 'DRIED ANCHOVIES', 'Dilis Crispy Fry', 'Encrasicholina punctifer',
   'Grade-A sun-dried silver anchovies. Unsalted or lightly salted options with whole intact bodies, perfect for candied snacks or crispy breakfasts.',
   1000, 600.00, 60, 'Anchovies & Dilis', 'images/products/dried-anchovies.png', false,
   'Medellin, Cebu', 'Crispy, mild salty bite, clean sea aroma',
   'Toast in oil with peanuts and chili flakes, or glaze with brown sugar and sesame seeds for sweet-spicy dilis.',
   '{250, 500, 1000}'),
  (5, 'DRIED KATAMBAK', 'Katambak / Bridled Spinecheek', 'Lethrinus nebulosus',
   'Split and butterfly-cut emperor bream sun-cured with unrefined rock salt. Thick, flavorful meat with pristine white flaky texture.',
   1000, 700.00, 20, 'Whitefish & Snappers', 'images/products/dried-katambak.png', false,
   'San Remigio, Cebu', 'Hearty, clean salt profile, flaky and savory',
   'Pan fry in moderate vegetable oil until outer skin is crispy while meat remains tender. Pair with sliced green mangoes.',
   '{250, 500, 1000}'),
  (6, 'DRIED FLOUNDER', 'Dapa / Dried Flatfish', 'Paralichthys olivaceus',
   'Hand-selected flounder flatfish cured immediately upon harbor arrival. Delivers a uniquely delicate, thin meat that turns remarkably brittle and chips-like.',
   1000, 700.00, 18, 'Flatfish', 'images/products/dried-flounder.png', true,
   'Bantayan Island, Cebu', 'Ultra-crisp, light salinity, delicate fish aroma',
   'Flash fry in high-heat coconut oil for 1 minute until glass-crisp. Pairs wonderfully with tomato and shallot salad.',
   '{250, 500, 1000}'),
  (7, 'BISUGO', 'Threadfin Bream Pinwheel', 'Nemipterus japonicus',
   'Sunburst circular presentation of threadfin breams. Cleaned, butterflied, and arranged in traditional wheel drying arrays under tropical sun.',
   1000, 550.00, 35, 'Whitefish & Snappers', 'images/products/bisugo.png', false,
   'Daanbantayan, Cebu', 'Nutty, medium salt, firm golden meat',
   'Fry each wheel intact for a stunning breakfast centerpiece. Dip in sinamak spiced vinegar.',
   '{250, 500, 1000}'),
  (8, 'DALAGANG BUKID', 'Yellowtail Fusilier', 'Caesio cuning',
   'Whole split reef fusilier with natural golden-yellow hues preserved during the gentle solar drying cycle. Substantial meat ideal for sharing.',
   1000, 500.00, 22, 'Whitefish & Snappers', 'images/products/dalagang-bukid.png', false,
   'Malapascua, Cebu', 'Mild sweetness, rich savory skin, dense flaky bite',
   'Grill lightly over charcoal or shallow fry until edges turn bronze.',
   '{250, 500, 1000}'),
  (9, 'DRIED HERRING', 'Tuyô / Tamban Smoked & Salted', 'Sardinella fimbriata',
   'The quintessential Philippine staple. Deeply salted, whole dried herrings with robust, unmistakable aroma and intense umami depth.',
   1000, 300.00, 75, 'Herring & Sardines', 'images/products/dried-herring.png', false,
   'Estancia & Cebu Waters', 'Robust salt punch, smoky notes, authentic rustic flavor',
   'Pan fry in enclosed pan to contain rich aromas. Sublime pairing with chocolate champorado or garlic sinangag.',
   '{250, 500, 1000}')
on conflict (id) do nothing;

-- Sync identity sequence after explicit seed IDs
select setval(
  pg_get_serial_sequence('public.dried_fish_items', 'id'),
  coalesce((select max(id) from public.dried_fish_items), 1)
);

-- Seed 8 customer reviews (skips any already present by id)
insert into public.product_reviews (id, product_id, author_name, rating, title, comment, created_at)
values
  (1, 1, 'Maria Santos', 5, 'Best danggit outside Bantayan',
   'Crispy fins, clean salt, and arrived vacuum-sealed fresh. Flash-fried for a minute and served with sukang maanghang.',
   now() - interval '2 days'),
  (2, 1, 'Jose Ramirez', 4, 'Great, a touch salty',
   'Excellent texture and crunch. I rinse it briefly before frying to tame the saltiness. Will order again.',
   now() - interval '9 days'),
  (3, 2, 'Aileen Cruz', 5, 'Sweet and chewy pusit',
   'Tastes like Bogo market sun-dried squid. Pan-fried 30 seconds per side and it stayed tender, not rubbery.',
   now() - interval '5 days'),
  (4, 4, 'Mark Villanueva', 5, 'Crispy dilis for breakfast',
   'Whole intact dilis, perfect for sweet-spicy glaze with peanuts. Kids finished the whole batch.',
   now() - interval '12 days'),
  (5, 9, 'Lola Nena', 4, 'Tuyo like home',
   'Authentic robust tuyo flavor. Fry with the lid on as advised. Perfect with champorado.',
   now() - interval '20 days'),
  (6, 6, 'Kevin Tan', 5, 'Flounder chips',
   'Turned glass-crisp in coconut oil. Light salinity, great with tomato and shallot salad.',
   now() - interval '7 days'),
  (7, null, 'Teresa Beth', 5, 'Fast shipping to Miami',
   'Ordered mixed 3kg for our restaurant. FDA-compliant packs, well-labeled, arrived in 4 days. Excellent support.',
   now() - interval '3 days'),
  (8, null, 'Dan Meyer', 4, 'Great quality, wish for more',
   'Overall quality is top-notch and packaging is excellent. Would love larger bulk discounts for wholesale.',
   now() - interval '15 days')
on conflict (id) do nothing;

select setval(
  pg_get_serial_sequence('public.product_reviews', 'id'),
  coalesce((select max(id) from public.product_reviews), 1)
);
