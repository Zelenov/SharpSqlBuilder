﻿using System;

namespace SharpSqlBuilder.Maps
{
    public class SqlColumn
    {
        public bool IsKey { get; set; }
        public bool IsUpdatable { get; set; } = true;
        public bool IsAutoGenerated { get; set; }
        public string ColumnName { get; set; }
        public string ForeignKey { get; set; }
        public string PropertyName { get; set; }
        public SqlTable Parent { get; set; }

        public SqlColumn Key()
        {
            IsKey = true;
            return DoNotUpdate();
        }
        public SqlColumn DoNotUpdate()
        {
            IsUpdatable = false;
            return this;
        }
        public SqlColumn WithForeignKey(string name)
        {
            ForeignKey = name;
            return this;
        }
        public SqlColumn Auto()
        {
            IsAutoGenerated = true;
            return this;
        }
    }
}