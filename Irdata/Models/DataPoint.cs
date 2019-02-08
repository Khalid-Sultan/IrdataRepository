using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Irdata.Models
{
     //DataContract for Serializing Data - required to serve in JSON format
    [DataContract]
    public class DataPoint
    {
        //public DataPoint(double x, double y)
        //{
        public DataPoint(double y,string name, bool exploded)
        {
            //this.X = x;
            this.Y = y;
            this.name = name;
            this.exploded = exploded;
        }

        ////Explicitly setting the name to be used while serializing to JSON.
        //[DataMember(Name = "x")]
        //public Nullable<double> X = null;

        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "y")]
        public Nullable<double> Y = null;
        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "name")]
        public string name = null;
        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "exploded")]
        public Nullable<bool> exploded = null;
    }
}