-- 1. Create the generic function that broadcasts the table name
CREATE OR REPLACE FUNCTION notify_table_update()
RETURNS trigger AS $$
BEGIN
  -- We broadcast the table name to a channel named 'bready_updates'
  PERFORM pg_notify('bready_updates', TG_TABLE_NAME);
  RETURN NEW;
END;
$$ LANGUAGE plpgsql;

-- 2. Drop existing triggers just in case this is re-run
DROP TRIGGER IF EXISTS trigger_notify_shelters ON shelters;
DROP TRIGGER IF EXISTS trigger_notify_inventory ON inventory_items;
DROP TRIGGER IF EXISTS trigger_notify_dispatch ON dispatch_logs;

-- 3. Attach the trigger to the Shelters table
CREATE TRIGGER trigger_notify_shelters
AFTER INSERT OR UPDATE OR DELETE ON shelters
FOR EACH ROW EXECUTE FUNCTION notify_table_update();

-- 4. Attach the trigger to the Inventory table
CREATE TRIGGER trigger_notify_inventory
AFTER INSERT OR UPDATE OR DELETE ON inventory_items
FOR EACH ROW EXECUTE FUNCTION notify_table_update();

-- 5. Attach the trigger to the Dispatch Logs table
CREATE TRIGGER trigger_notify_dispatch
AFTER INSERT OR UPDATE OR DELETE ON dispatch_logs
FOR EACH ROW EXECUTE FUNCTION notify_table_update();

-- Test it:
-- UPDATE shelters SET status = 'Open' WHERE shelter_id = 1;
