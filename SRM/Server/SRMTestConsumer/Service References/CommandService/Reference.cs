﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SRMTestConsumer.CommandService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CommandRequest", Namespace="http://schemas.datacontract.org/2004/07/SRMCommandService")]
    [System.SerializableAttribute()]
    public partial class CommandRequest : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private byte[][] binaryParamField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int numBinaryParamField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int numParamField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string serviceCommandField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string serviceNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int[] sizeBinaryParamField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string[] valueParamField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public byte[][] binaryParam {
            get {
                return this.binaryParamField;
            }
            set {
                if ((object.ReferenceEquals(this.binaryParamField, value) != true)) {
                    this.binaryParamField = value;
                    this.RaisePropertyChanged("binaryParam");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int numBinaryParam {
            get {
                return this.numBinaryParamField;
            }
            set {
                if ((this.numBinaryParamField.Equals(value) != true)) {
                    this.numBinaryParamField = value;
                    this.RaisePropertyChanged("numBinaryParam");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int numParam {
            get {
                return this.numParamField;
            }
            set {
                if ((this.numParamField.Equals(value) != true)) {
                    this.numParamField = value;
                    this.RaisePropertyChanged("numParam");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string serviceCommand {
            get {
                return this.serviceCommandField;
            }
            set {
                if ((object.ReferenceEquals(this.serviceCommandField, value) != true)) {
                    this.serviceCommandField = value;
                    this.RaisePropertyChanged("serviceCommand");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string serviceName {
            get {
                return this.serviceNameField;
            }
            set {
                if ((object.ReferenceEquals(this.serviceNameField, value) != true)) {
                    this.serviceNameField = value;
                    this.RaisePropertyChanged("serviceName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int[] sizeBinaryParam {
            get {
                return this.sizeBinaryParamField;
            }
            set {
                if ((object.ReferenceEquals(this.sizeBinaryParamField, value) != true)) {
                    this.sizeBinaryParamField = value;
                    this.RaisePropertyChanged("sizeBinaryParam");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string[] valueParam {
            get {
                return this.valueParamField;
            }
            set {
                if ((object.ReferenceEquals(this.valueParamField, value) != true)) {
                    this.valueParamField = value;
                    this.RaisePropertyChanged("valueParam");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CommandResponse", Namespace="http://schemas.datacontract.org/2004/07/SRMCommandService")]
    [System.SerializableAttribute()]
    public partial class CommandResponse : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private byte[][] binaryDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string codeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string dataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int numBinaryDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int[] sizeBinaryDataField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public byte[][] binaryData {
            get {
                return this.binaryDataField;
            }
            set {
                if ((object.ReferenceEquals(this.binaryDataField, value) != true)) {
                    this.binaryDataField = value;
                    this.RaisePropertyChanged("binaryData");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string code {
            get {
                return this.codeField;
            }
            set {
                if ((object.ReferenceEquals(this.codeField, value) != true)) {
                    this.codeField = value;
                    this.RaisePropertyChanged("code");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string data {
            get {
                return this.dataField;
            }
            set {
                if ((object.ReferenceEquals(this.dataField, value) != true)) {
                    this.dataField = value;
                    this.RaisePropertyChanged("data");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int numBinaryData {
            get {
                return this.numBinaryDataField;
            }
            set {
                if ((this.numBinaryDataField.Equals(value) != true)) {
                    this.numBinaryDataField = value;
                    this.RaisePropertyChanged("numBinaryData");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int[] sizeBinaryData {
            get {
                return this.sizeBinaryDataField;
            }
            set {
                if ((object.ReferenceEquals(this.sizeBinaryDataField, value) != true)) {
                    this.sizeBinaryDataField = value;
                    this.RaisePropertyChanged("sizeBinaryData");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="CommandService.ICommandService")]
    public interface ICommandService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICommandService/ExecuteCommand", ReplyAction="http://tempuri.org/ICommandService/ExecuteCommandResponse")]
        SRMTestConsumer.CommandService.CommandResponse[] ExecuteCommand(SRMTestConsumer.CommandService.CommandRequest req);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICommandService/ExecuteCommand", ReplyAction="http://tempuri.org/ICommandService/ExecuteCommandResponse")]
        System.Threading.Tasks.Task<SRMTestConsumer.CommandService.CommandResponse[]> ExecuteCommandAsync(SRMTestConsumer.CommandService.CommandRequest req);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ICommandServiceChannel : SRMTestConsumer.CommandService.ICommandService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CommandServiceClient : System.ServiceModel.ClientBase<SRMTestConsumer.CommandService.ICommandService>, SRMTestConsumer.CommandService.ICommandService {
        
        public CommandServiceClient() {
        }
        
        public CommandServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CommandServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CommandServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CommandServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public SRMTestConsumer.CommandService.CommandResponse[] ExecuteCommand(SRMTestConsumer.CommandService.CommandRequest req) {
            return base.Channel.ExecuteCommand(req);
        }
        
        public System.Threading.Tasks.Task<SRMTestConsumer.CommandService.CommandResponse[]> ExecuteCommandAsync(SRMTestConsumer.CommandService.CommandRequest req) {
            return base.Channel.ExecuteCommandAsync(req);
        }
    }
}