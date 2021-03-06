using System;
using System.Linq;
using System.Collections.Generic;

namespace DataService.Types
{
    public class SampleSet
    {
        private Guid _id;
        private List<Segment> _segments;
        private string _data_uri;
        private string _ref_uri;
        private int[] _idx_vector;
        private int[] _rep_vector;
        private int[] _delete_vector;
        private List<String> _ref_names;

        public List<Segment> Segments { get => _segments; set => _segments = value; }
        public Guid ID { get => _id; set => _id = value; }
        public string DataUri { get => _data_uri; set => _data_uri = value; }
        public string RefUri { get => _ref_uri; set => _ref_uri = value; }
        public List<String> ConstituentNames { get => _ref_names; set => _ref_names = value; }
        
        public int[] IdxVector { get => _idx_vector; set => _idx_vector = value; }
        public int[] RepVector { get => _rep_vector; set => _rep_vector = value; }
        public int[] DeleteVector { get => _delete_vector; set => _delete_vector = value; }
    }

    public class Segment
    {
        private double _first;
        private double _last;
        private double _delta;

        public Segment(double first, double last, double delta)
        {
            this.first = first;
            this.last = last;
            this.delta = delta;
        }

        public double first { get => _first; set => _first = value; }

        public double last { get => _last; set => _last = value; }

        public double delta { get => _delta; set => _delta = value; }
    }
}