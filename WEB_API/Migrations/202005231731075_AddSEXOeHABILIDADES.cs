namespace WEB_API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSEXOeHABILIDADES : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Funcionarios", "SEXO", c => c.String());
            AddColumn("dbo.Funcionarios", "HABILIDADES", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Funcionarios", "HABILIDADES");
            DropColumn("dbo.Funcionarios", "SEXO");
        }
    }
}
