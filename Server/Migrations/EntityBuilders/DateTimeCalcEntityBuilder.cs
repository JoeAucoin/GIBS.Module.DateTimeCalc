using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;
using Oqtane.Migrations.EntityBuilders;

namespace GIBS.Module.DateTimeCalc.Migrations.EntityBuilders
{
    public class DateTimeCalcEntityBuilder : AuditableBaseEntityBuilder<DateTimeCalcEntityBuilder>
    {
        private const string _entityTableName = "GIBSDateTimeCalc";
        private readonly PrimaryKey<DateTimeCalcEntityBuilder> _primaryKey = new("PK_GIBSDateTimeCalc", x => x.DateTimeCalcId);
        private readonly ForeignKey<DateTimeCalcEntityBuilder> _moduleForeignKey = new("FK_GIBSDateTimeCalc_Module", x => x.ModuleId, "Module", "ModuleId", ReferentialAction.Cascade);

        public DateTimeCalcEntityBuilder(MigrationBuilder migrationBuilder, IDatabase database) : base(migrationBuilder, database)
        {
            EntityTableName = _entityTableName;
            PrimaryKey = _primaryKey;
            ForeignKeys.Add(_moduleForeignKey);
        }

        protected override DateTimeCalcEntityBuilder BuildTable(ColumnsBuilder table)
        {
            DateTimeCalcId = AddAutoIncrementColumn(table,"DateTimeCalcId");
            ModuleId = AddIntegerColumn(table,"ModuleId");
            Name = AddMaxStringColumn(table,"Name");
            StartDate = AddDateTimeColumn(table,"StartDate",true);
            EndDate = AddDateTimeColumn(table,"EndDate",true);
            ShowYears = AddBooleanColumn(table,"ShowYears");
            ShowMonths = AddBooleanColumn(table,"ShowMonths");
            ShowWeeks = AddBooleanColumn(table,"ShowWeeks");
            ShowDays = AddBooleanColumn(table,"ShowDays");
            ShowHours = AddBooleanColumn(table,"ShowHours");
            ShowMinutes = AddBooleanColumn(table,"ShowMinutes");
            ShowSeconds = AddBooleanColumn(table,"ShowSeconds");
            Template = AddMaxStringColumn(table,"Template",true);
            AddAuditableColumns(table);
            return this;
        }

        public OperationBuilder<AddColumnOperation> DateTimeCalcId { get; set; }
        public OperationBuilder<AddColumnOperation> ModuleId { get; set; }
        public OperationBuilder<AddColumnOperation> Name { get; set; }
        public OperationBuilder<AddColumnOperation> StartDate { get; set; }
        public OperationBuilder<AddColumnOperation> EndDate { get; set; }
        public OperationBuilder<AddColumnOperation> ShowYears { get; set; }
        public OperationBuilder<AddColumnOperation> ShowMonths { get; set; }
        public OperationBuilder<AddColumnOperation> ShowWeeks { get; set; }
        public OperationBuilder<AddColumnOperation> ShowDays { get; set; }
        public OperationBuilder<AddColumnOperation> ShowHours { get; set; }
        public OperationBuilder<AddColumnOperation> ShowMinutes { get; set; }
        public OperationBuilder<AddColumnOperation> ShowSeconds { get; set; }
        public OperationBuilder<AddColumnOperation> Template { get; set; }
    }
}
