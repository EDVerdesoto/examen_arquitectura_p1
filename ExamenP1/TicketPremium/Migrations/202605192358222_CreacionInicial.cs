namespace TicketPremium.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreacionInicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FACTURA_DETALLE",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FacturaId = c.Int(nullable: false),
                        CodigoLocalidad = c.String(nullable: false, maxLength: 50),
                        Cantidad = c.Int(nullable: false),
                        PrecioUnitario = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalLocalidad = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FACTURA_CABECERA", t => t.FacturaId, cascadeDelete: true)
                .Index(t => t.FacturaId);
            
            CreateTable(
                "dbo.FACTURA_CABECERA",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PartidoCodigo = c.Int(nullable: false),
                        PartidoDescripcion = c.String(nullable: false, maxLength: 200),
                        FechaPartido = c.DateTime(nullable: false),
                        FechaCompra = c.DateTime(nullable: false),
                        Subtotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Iva = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UsuarioId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.USUARIO", t => t.UsuarioId, cascadeDelete: true)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.USUARIO",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NombreDeUsuario = c.String(nullable: false, maxLength: 50),
                        Clave = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FACTURA_DETALLE", "FacturaId", "dbo.FACTURA_CABECERA");
            DropForeignKey("dbo.FACTURA_CABECERA", "UsuarioId", "dbo.USUARIO");
            DropIndex("dbo.FACTURA_CABECERA", new[] { "UsuarioId" });
            DropIndex("dbo.FACTURA_DETALLE", new[] { "FacturaId" });
            DropTable("dbo.USUARIO");
            DropTable("dbo.FACTURA_CABECERA");
            DropTable("dbo.FACTURA_DETALLE");
        }
    }
}
