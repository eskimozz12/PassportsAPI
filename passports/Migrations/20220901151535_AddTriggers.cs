using Microsoft.EntityFrameworkCore.Migrations;
using System.Text;

#nullable disable

namespace PassportsAPI.Migrations
{
    public partial class AddTriggers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sb = new StringBuilder();
            sb.AppendLine("CREATE OR REPLACE FUNCTION history()");
            sb.AppendLine("RETURNS TRIGGER");
            sb.AppendLine("AS $$");
            sb.AppendLine("BEGIN");
            sb.AppendLine(" IF TG_OP = 'INSERT' THEN");
            sb.AppendLine("  INSERT INTO history(isactive,changetime,passportid) VALUES (NEW.isactive, NEW.changetime, NEW.id);");
            sb.AppendLine("  RETURN NEW;");
            sb.AppendLine(" ELSIF TG_OP = 'UPDATE' THEN");
            sb.AppendLine("  IF NEW.isactive <> OLD.isactive THEN");
            sb.AppendLine("   INSERT INTO history(isactive,changetime,passportid) VALUES (NEW.isactive, NEW.changetime, OLD.id);");
            sb.AppendLine("  END IF;");
            sb.AppendLine("  RETURN NEW;");
            sb.AppendLine(" END IF;");
            sb.AppendLine("END;");
            sb.AppendLine("$$ LANGUAGE plpgsql;");

            migrationBuilder.Sql(sb.ToString());

            sb.Clear();

            sb.AppendLine("CREATE OR REPLACE TRIGGER tr_history AFTER UPDATE OR INSERT ON passports");
            sb.AppendLine("FOR EACH ROW EXECUTE PROCEDURE history();");

            migrationBuilder.Sql(sb.ToString());


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
