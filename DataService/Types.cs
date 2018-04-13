using System;
using System.Collections.Generic;

namespace DataService.Types
{
    public class SampleSet
    {
        private Guid _id;
        private List<Segment> _inBase;
        private string _data_uri;
        private string _ref_uri;

        public List<Segment> inBase { get => _inBase; set => _inBase = value; }
        public Guid ID { get => _id; set => _id = value; }
        public string dataUri { get => _data_uri; set => _data_uri = value; }
        public string refUri { get => _ref_uri; set => _ref_uri = value; }
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