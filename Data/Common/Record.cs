using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoProject.Data.Common
{
    public abstract class Record
    {
        private Type currentType;

        private bool _isHistRecord = false;
        [NotMapped]
        public bool IsHistRecord => _isHistRecord;
        private string tableName = default!;

        public virtual int Id { get; set; }
        public int Ver { get; set; }
        public int? HistActionPerformedById { get; set; }
        public string HistActionPerformedBy { get; set; } = "";
        public string HistActionType { get; set; } = "i";
        [NotMapped]
        public int HistActionTypeOrder => HistActionType == "i" ? 1 : HistActionType == "u" ? 2 : 3;
        public DateTime HistActionDate { get; set; }

        public string HistActionDateString {
            get => HistActionDate.ToString("dd/MM/yyyy HH:mm:ss");
            set {} //EF required a setter and I wanted the field "readonly" and in sync with HistActionDate, but DB searchable for filters
        }

        public Record(){
            currentType = this.GetType();
            var ihists = (IEnumerable<Type>)currentType.GetInterfaces().Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IHistFor<>));
            _isHistRecord = ihists.Any();
            tableName = GetPlural((_isHistRecord ? ihists.First().GetGenericArguments()[0] : currentType).Name) + (_isHistRecord ? "_Hist" : "");
        }

        private static Dictionary<Type, Func<object, object>> histBuilders;
        
        static string GetPlural(string forEntity)
            =>  (forEntity.EndsWith("y") ? forEntity.Substring(0, forEntity.Length - 1) + "ies"
                : forEntity.EndsWith("Series") ? forEntity + "Collection"
                : forEntity + "s").Split(".").Last();

        static Record()
        {
            histBuilders = new Dictionary<Type, Func<object, object>>();

            //reflect through all and create histBuilder delegates that receive a record of type "dict key" and return a hist object for that type.
            foreach(var t in typeof(Record).Assembly.GetTypes()){
                var histInterfaces = t.GetInterfaces().Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IHistFor<>));
                
                if (histInterfaces.Count() == 1){
                    var mainType = histInterfaces.First().GetGenericArguments()[0];
                    if( ! t.IsSubclassOf(mainType) && t.BaseType != mainType.BaseType) {
                        throw new InvalidProgramException($@"{t.FullName} cannot be a hist record for "
                            + $@"{mainType.FullName} because it is not a subclass of it, nor do they have the same base type.");
                    }

                    histBuilders.Add(mainType, CloneHelper.GetCloneMatchingScalarPropertiesFunction(mainType, cloneType: t));
                }
                else if(histInterfaces.Count() > 1) throw new InvalidProgramException($@"{t.FullName} cannot implement {typeof(IHistFor<>).FullName} multiple times.");
            }
        }

        [NotMapped]
        public bool CanGetHistRecord => histBuilders.ContainsKey(currentType);
        public Record? GetHistRecord() => (CanGetHistRecord ? (Record)histBuilders[currentType](this) : null);

        public virtual void Configure(ModelBuilder modelBuilder, ILogger logger){
            //default code, including the entity configuration with colOrder and all...

            logger.LogDebug($@"IsHistRecord: {IsHistRecord}, RecordType: {currentType}");

            var entityBuilder = modelBuilder.Entity(currentType);
            
            entityBuilder.Property(nameof(HistActionPerformedById)).HasColumnName("who");
            entityBuilder.Property(nameof(HistActionPerformedBy)).HasColumnName("whoName");
            entityBuilder.Property(nameof(HistActionType)).HasColumnName("what").HasColumnType("CHAR(1)");
            entityBuilder.Property(nameof(HistActionDate)).HasColumnName("when");
            entityBuilder.Property(nameof(HistActionDateString)).HasColumnName("when_str");

            if(IsHistRecord){
                entityBuilder.HasKey(nameof(Record.Id), nameof(Record.Ver));
            }
            else {
                entityBuilder.HasKey(nameof(Record.Id));
            }
            
            entityBuilder.ToTable(tableName);
        }
    }
}