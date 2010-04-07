using System;
using System.Collections.Generic;
using GeoAPI.Coordinates;
using GeoAPI.Geometries;
using GeoAPI.IO.WellKnownText;
using GisSharpBlog.NetTopologySuite.Geometries;
using Coord = NetTopologySuite.Coordinates.Simple.Coordinate;
using CoordSeq = NetTopologySuite.Coordinates.Simple.CoordinateSequence;
using CoordFac = NetTopologySuite.Coordinates.Simple.CoordinateFactory;
using CoordSeqFac = NetTopologySuite.Coordinates.Simple.CoordinateSequenceFactory;

namespace NetTopologySuite.Tests.NUnit
{

public class GeometryUtils 
{
    public static readonly ICoordinateSequenceFactory<Coord> CoordSeqFac;
    public static readonly CoordFac CoordFac;
    public static readonly IGeometryFactory<Coord> GeometryFactory;

        //TODO: allow specifying GeometryFactory
	static GeometryUtils()
	{
	    CoordFac = new CoordFac(PrecisionModelType.DoubleFloating);
	    CoordSeqFac = new CoordSeqFac(CoordFac);
	    GeometryFactory = new GeometryFactory<Coord>(CoordSeqFac);
	    Reader = GeometryFactory.WktReader;
	}

    public static readonly IWktGeometryReader<Coord> Reader;
	
  public static IEnumerable<IGeometry<Coord>> ReadWKT(String[] inputWKT)
  {
      foreach (string s in inputWKT)
          yield return Reader.Read(s);
  }
  
  public static IGeometry<Coord> ReadWKT(String inputWKT)
  {
  	return Reader.Read(inputWKT);
  }
  
  //public static IEnumerable<IGeometry<Coord>> ReadWKTFile(String filename) 
  //{
  //  WKTFileReader fileRdr = new WKTFileReader(filename, reader);
  //  List geoms = fileRdr.read();
  //  return geoms;
  //}
  

  public static Boolean IsEqual(IGeometry<Coord> a, IGeometry<Coord> b)
  {
  	IGeometry<Coord> a2 = Normalize(a);
  	IGeometry<Coord> b2 = Normalize(b);
  	return a2.EqualsExact(b2);
  }
  
  public static IGeometry<Coord> Normalize(IGeometry<Coord> g)
  {
  	 IGeometry<Coord>  g2 = g.Clone();
  	g2.Normalize();
  	return g2;
  }
}
}