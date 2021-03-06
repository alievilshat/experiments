﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Devart Entity Developer tool using Entity Framework EntityObject template.
// Code is generated on: 6/28/2014 1:37:59 PM
//
// Changes to this file may cause incorrect behavior and will be lost if
// the code is regenerated.
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Reflection;
using System.Linq.Expressions;
using System.Data.EntityClient;
using System.Data.Metadata.Edm;
using System.Data.Objects;
using System.Data.Objects.DataClasses;

[assembly: EdmSchemaAttribute()]
#region EDM Relationship Metadata


#endregion

namespace ScriptModuleModel
{

    #region ScriptModuleEntities

    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class ScriptModuleEntities : ObjectContext
    {
        #region Constructors

        /// <summary>
        /// Initialize a new ScriptModuleEntities object.
        /// </summary>
        public ScriptModuleEntities() : 
                base(@"name=Bodyview3EntitiesConnectionString", "ScriptModuleEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }

        /// <summary>
        /// Initializes a new ScriptModuleEntities object using the connection string found in the 'ScriptModuleEntities' section of the application configuration file.
        /// </summary>
        public ScriptModuleEntities(string connectionString) : 
                base(connectionString, "ScriptModuleEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }

        /// <summary>
        /// Initialize a new ScriptModuleEntities object.
        /// </summary>
        public ScriptModuleEntities(EntityConnection connection) : base(connection, "ScriptModuleEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }

        #endregion

        #region Partial Methods

        partial void OnContextCreated();

        #endregion

        #region ObjectSet Properties

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<ScriptRow> ScriptRows
        {
            get
            {
                if ((_ScriptRows == null))
                {
                    _ScriptRows = base.CreateObjectSet<ScriptRow>("ScriptRows");
                }
                return _ScriptRows;
            }
        }
        private ObjectSet<ScriptRow> _ScriptRows;

        #endregion
        #region AddTo Methods

        /// <summary>
        /// Deprecated Method for adding a new object to the ScriptRows EntitySet.
        /// </summary>
        public void AddToScriptRows(ScriptRow scriptRow)
        {
            base.AddObject("ScriptRows", scriptRow);
        }

        #endregion
    }

    #endregion
}

namespace ScriptModuleModel
{

    /// <summary>
    /// There are no comments for ScriptModuleModel.ScriptRow in the schema.
    /// </summary>
    /// <KeyProperties>
    /// Scriptid
    /// </KeyProperties>
    [EdmEntityTypeAttribute(NamespaceName="ScriptModuleModel", Name="ScriptRow")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class ScriptRow : EntityObject    {
        #region Factory Method

        /// <summary>
        /// Create a new ScriptRow object.
        /// </summary>
        /// <param name="scriptid">Initial value of Scriptid.</param>
        public static ScriptRow CreateScriptRow(int scriptid)
        {
            ScriptRow scriptRow = new ScriptRow();
            scriptRow.Scriptid = scriptid;
            return scriptRow;
        }

        #endregion

        #region Properties
    
        /// <summary>
        /// There are no comments for Scriptid in the schema.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public int Scriptid
        {
            get
            {
                int value = _Scriptid;
                OnGetScriptid(ref value);
                return value;
            }
            set
            {
                if (_Scriptid != value)
                {
                  OnScriptidChanging(ref value);
                  ReportPropertyChanging("Scriptid");
                  _Scriptid = StructuralObject.SetValidValue(value);
                  ReportPropertyChanged("Scriptid");
                  OnScriptidChanged();
              }
            }
        }
        private int _Scriptid;
        partial void OnGetScriptid(ref int value);
        partial void OnScriptidChanging(ref int value);
        partial void OnScriptidChanged();
    
        /// <summary>
        /// There are no comments for Scriptname in the schema.
        /// </summary>
        [EdmScalarPropertyAttribute()]
        [DataMemberAttribute()]
        public string Scriptname
        {
            get
            {
                string value = _Scriptname;
                OnGetScriptname(ref value);
                return value;
            }
            set
            {
                if (_Scriptname != value)
                {
                  OnScriptnameChanging(ref value);
                  ReportPropertyChanging("Scriptname");
                  _Scriptname = StructuralObject.SetValidValue(value, true);
                  ReportPropertyChanged("Scriptname");
                  OnScriptnameChanged();
              }
            }
        }
        private string _Scriptname;
        partial void OnGetScriptname(ref string value);
        partial void OnScriptnameChanging(ref string value);
        partial void OnScriptnameChanged();
    
        /// <summary>
        /// There are no comments for Scripttext in the schema.
        /// </summary>
        [EdmScalarPropertyAttribute()]
        [DataMemberAttribute()]
        public string Scripttext
        {
            get
            {
                string value = _Scripttext;
                OnGetScripttext(ref value);
                return value;
            }
            set
            {
                if (_Scripttext != value)
                {
                  OnScripttextChanging(ref value);
                  ReportPropertyChanging("Scripttext");
                  _Scripttext = StructuralObject.SetValidValue(value, true);
                  ReportPropertyChanged("Scripttext");
                  OnScripttextChanged();
              }
            }
        }
        private string _Scripttext;
        partial void OnGetScripttext(ref string value);
        partial void OnScripttextChanging(ref string value);
        partial void OnScripttextChanged();
    
        /// <summary>
        /// There are no comments for Parent in the schema.
        /// </summary>
        [EdmScalarPropertyAttribute()]
        [DataMemberAttribute()]
        public global::System.Nullable<int> Parent
        {
            get
            {
                global::System.Nullable<int> value = _Parent;
                OnGetParent(ref value);
                return value;
            }
            set
            {
                if (_Parent != value)
                {
                  OnParentChanging(ref value);
                  ReportPropertyChanging("Parent");
                  _Parent = StructuralObject.SetValidValue(value);
                  ReportPropertyChanged("Parent");
                  OnParentChanged();
              }
            }
        }
        private global::System.Nullable<int> _Parent;
        partial void OnGetParent(ref global::System.Nullable<int> value);
        partial void OnParentChanging(ref global::System.Nullable<int> value);
        partial void OnParentChanged();

        #endregion
    }

}
