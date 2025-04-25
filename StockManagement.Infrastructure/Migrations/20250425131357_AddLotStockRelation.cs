using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddLotStockRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Entrepots",
                columns: table => new
                {
                    IdEntrepot = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adresse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Localisation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entrepots", x => x.IdEntrepot);
                });

            migrationBuilder.CreateTable(
                name: "Fournisseurs",
                columns: table => new
                {
                    IdFournisseur = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adresse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fournisseurs", x => x.IdFournisseur);
                });

            migrationBuilder.CreateTable(
                name: "Utilisateurs",
                columns: table => new
                {
                    IdUtilisateur = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilisateurs", x => x.IdUtilisateur);
                });

            migrationBuilder.CreateTable(
                name: "Capteurs",
                columns: table => new
                {
                    IdCapteur = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Emplacement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntrepotId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Capteurs", x => x.IdCapteur);
                    table.ForeignKey(
                        name: "FK_Capteurs_Entrepots_EntrepotId",
                        column: x => x.EntrepotId,
                        principalTable: "Entrepots",
                        principalColumn: "IdEntrepot",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    IdArticle = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Designation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodeArticle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodeBarre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unite = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FournisseurId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.IdArticle);
                    table.ForeignKey(
                        name: "FK_Articles_Fournisseurs_FournisseurId",
                        column: x => x.FournisseurId,
                        principalTable: "Fournisseurs",
                        principalColumn: "IdFournisseur",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoriqueTemperatures",
                columns: table => new
                {
                    IdHistorique = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateMesure = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Valeur = table.Column<float>(type: "real", nullable: false),
                    CapteurId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoriqueTemperatures", x => x.IdHistorique);
                    table.ForeignKey(
                        name: "FK_HistoriqueTemperatures_Capteurs_CapteurId",
                        column: x => x.CapteurId,
                        principalTable: "Capteurs",
                        principalColumn: "IdCapteur",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArticleFournisseurs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticleId = table.Column<int>(type: "int", nullable: false),
                    FournisseurId = table.Column<int>(type: "int", nullable: false),
                    Prix = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateLivraison = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleFournisseurs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticleFournisseurs_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "IdArticle",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ArticleFournisseurs_Fournisseurs_FournisseurId",
                        column: x => x.FournisseurId,
                        principalTable: "Fournisseurs",
                        principalColumn: "IdFournisseur",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Lots",
                columns: table => new
                {
                    IdLot = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArticleId = table.Column<int>(type: "int", nullable: false),
                    DateExpiration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FournisseurId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lots", x => x.IdLot);
                    table.ForeignKey(
                        name: "FK_Lots_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "IdArticle",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lots_Fournisseurs_FournisseurId",
                        column: x => x.FournisseurId,
                        principalTable: "Fournisseurs",
                        principalColumn: "IdFournisseur",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MouvementStocks",
                columns: table => new
                {
                    IdMouvement = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateMouvement = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Quantite = table.Column<int>(type: "int", nullable: false),
                    LotId = table.Column<int>(type: "int", nullable: false),
                    EntrepotSourceId = table.Column<int>(type: "int", nullable: true),
                    EntrepotDestinationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MouvementStocks", x => x.IdMouvement);
                    table.ForeignKey(
                        name: "FK_MouvementStocks_Entrepots_EntrepotDestinationId",
                        column: x => x.EntrepotDestinationId,
                        principalTable: "Entrepots",
                        principalColumn: "IdEntrepot",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MouvementStocks_Entrepots_EntrepotSourceId",
                        column: x => x.EntrepotSourceId,
                        principalTable: "Entrepots",
                        principalColumn: "IdEntrepot",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MouvementStocks_Lots_LotId",
                        column: x => x.LotId,
                        principalTable: "Lots",
                        principalColumn: "IdLot",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    IdStock = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantite = table.Column<int>(type: "int", nullable: false),
                    EntrepotId = table.Column<int>(type: "int", nullable: false),
                    LotId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.IdStock);
                    table.ForeignKey(
                        name: "FK_Stocks_Entrepots_EntrepotId",
                        column: x => x.EntrepotId,
                        principalTable: "Entrepots",
                        principalColumn: "IdEntrepot",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stocks_Lots_LotId",
                        column: x => x.LotId,
                        principalTable: "Lots",
                        principalColumn: "IdLot",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AlerteStocks",
                columns: table => new
                {
                    IdAlerteStock = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateAlerte = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstResolue = table.Column<bool>(type: "bit", nullable: false),
                    StockId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlerteStocks", x => x.IdAlerteStock);
                    table.ForeignKey(
                        name: "FK_AlerteStocks_Stocks_StockId",
                        column: x => x.StockId,
                        principalTable: "Stocks",
                        principalColumn: "IdStock",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlerteStocks_StockId",
                table: "AlerteStocks",
                column: "StockId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleFournisseurs_ArticleId",
                table: "ArticleFournisseurs",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleFournisseurs_FournisseurId",
                table: "ArticleFournisseurs",
                column: "FournisseurId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_FournisseurId",
                table: "Articles",
                column: "FournisseurId");

            migrationBuilder.CreateIndex(
                name: "IX_Capteurs_EntrepotId",
                table: "Capteurs",
                column: "EntrepotId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoriqueTemperatures_CapteurId",
                table: "HistoriqueTemperatures",
                column: "CapteurId");

            migrationBuilder.CreateIndex(
                name: "IX_Lots_ArticleId",
                table: "Lots",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_Lots_FournisseurId",
                table: "Lots",
                column: "FournisseurId");

            migrationBuilder.CreateIndex(
                name: "IX_MouvementStocks_EntrepotDestinationId",
                table: "MouvementStocks",
                column: "EntrepotDestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_MouvementStocks_EntrepotSourceId",
                table: "MouvementStocks",
                column: "EntrepotSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_MouvementStocks_LotId",
                table: "MouvementStocks",
                column: "LotId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_EntrepotId",
                table: "Stocks",
                column: "EntrepotId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_LotId",
                table: "Stocks",
                column: "LotId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlerteStocks");

            migrationBuilder.DropTable(
                name: "ArticleFournisseurs");

            migrationBuilder.DropTable(
                name: "HistoriqueTemperatures");

            migrationBuilder.DropTable(
                name: "MouvementStocks");

            migrationBuilder.DropTable(
                name: "Utilisateurs");

            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.DropTable(
                name: "Capteurs");

            migrationBuilder.DropTable(
                name: "Lots");

            migrationBuilder.DropTable(
                name: "Entrepots");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Fournisseurs");
        }
    }
}
