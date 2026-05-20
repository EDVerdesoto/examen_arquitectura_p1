namespace FederacionDeFutbol.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreacionInicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LOCALIDAD_PARTIDO",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CodigoLocalidad = c.String(nullable: false, maxLength: 50),
                        Disponibilidad = c.Int(nullable: false),
                        Precio = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PartidoCodigo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PARTIDO_FUTBOL", t => t.PartidoCodigo, cascadeDelete: true)
                .Index(t => t.PartidoCodigo);
            
            CreateTable(
                "dbo.PARTIDO_FUTBOL",
                c => new
                    {
                        Codigo = c.Int(nullable: false, identity: true),
                        EquipoLocal = c.String(nullable: false, maxLength: 100),
                        EquipoVisita = c.String(nullable: false, maxLength: 100),
                        Fecha = c.DateTime(nullable: false),
                        Lugar = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.Codigo);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LOCALIDAD_PARTIDO", "PartidoCodigo", "dbo.PARTIDO_FUTBOL");
            DropIndex("dbo.LOCALIDAD_PARTIDO", new[] { "PartidoCodigo" });
            DropTable("dbo.PARTIDO_FUTBOL");
            DropTable("dbo.LOCALIDAD_PARTIDO");
        }
    }
}
