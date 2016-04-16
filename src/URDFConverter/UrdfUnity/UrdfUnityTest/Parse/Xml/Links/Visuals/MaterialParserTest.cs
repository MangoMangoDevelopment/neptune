using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Urdf.Models.Attributes;
using UrdfUnity.Parse.Xml.Links.Visuals;
using UrdfUnity.Urdf.Models.Links.Visuals;

namespace UrdfUnityTest.Parse.Xml.Links.Visuals
{
    [TestClass]
    public class MaterialParserTest
    {
        private static readonly Dictionary<string, Material> materialDictionary = new Dictionary<string, Material>();

        private readonly MaterialParser parser = new MaterialParser(materialDictionary);
        private readonly XmlDocument xmlDoc = new XmlDocument();
        

        [TestCleanup]
        public void CleanUp()
        {
            materialDictionary.Clear();
        }

        [TestMethod]
        public void ParseMaterialColor()
        {
            string name = "name";
            RgbAttribute rgb = new RgbAttribute(1, 1, 1);
            string xml = String.Format("<material name='{0}'><color rgb='{1} {2} {3}'/></material>",
                name, rgb.R, rgb.B, rgb.G);

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Material material = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(name, material.Name);
            Assert.AreEqual(rgb, material.Color.Rgb);
            Assert.IsNull(material.Texture);
        }

        [TestMethod]
        public void ParseMaterial()
        {
            string name = "name";
            RgbAttribute rgb = new RgbAttribute(1, 1, 1);
            string filename = "filename";
            string xml = String.Format("<material name='{0}'><color rgb='{1} {2} {3}'/><texture filename='{4}'/></material>",
                name, rgb.R, rgb.B, rgb.G, filename);

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Material material = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(name, material.Name);
            Assert.AreEqual(rgb, material.Color.Rgb);
            Assert.AreEqual(filename, material.Texture.FileName);
        }

        [TestMethod]
        public void ParseMaterialPredefined()
        {
            Material predefinedMaterial = new Material("predefined", new Color(new RgbAttribute(1, 2, 3)));
            string xml = String.Format("<material name='{0}'></material>", predefinedMaterial.Name);

            materialDictionary.Add(predefinedMaterial.Name, predefinedMaterial);
            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Material material = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(predefinedMaterial.Name, material.Name);
            Assert.AreEqual(predefinedMaterial.Color, material.Color);
            Assert.IsNull(material.Texture);
        }

        [TestMethod]
        public void ParseMaterialTexture()
        {
            string name = "name";
            string textureFilename = "filename";
            string xml = String.Format("<material name='{0}'><texture filename='{1}'/></material>", name, textureFilename);

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Material material = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(name, material.Name);
            Assert.AreEqual(textureFilename, material.Texture.FileName);
            Assert.IsNull(material.Color);
        }

        [TestMethod]
        public void ParseMaterialNoName()
        {
            string textureFilename = "filename";
            string xml = String.Format("<material><texture filename='{0}'/></material>", textureFilename);

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Material material = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(Material.DEFAULT_NAME, material.Name);
            Assert.AreEqual(textureFilename, material.Texture.FileName);
            Assert.IsNull(material.Color);
        }

        [TestMethod]
        public void ParseMaterialMalformedColor()
        {
            string name = "name";
            string xml = String.Format("<material name='{0}'><color></color></material>", name); // No color attributes

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Material material = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(name, material.Name);
            Assert.AreEqual(new Color(new RgbAttribute(0, 0, 0)), material.Color);
            Assert.IsNull(material.Texture);
        }

        [TestMethod]
        public void ParseMaterialMalformedTexture()
        {
            string name = "name";
            string xml = String.Format("<material name='{0}'><texture></texture></material>", name); // No texture attributes

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Material material = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(name, material.Name);
            Assert.IsNull(material.Color);
            Assert.AreEqual(Texture.DEFAULT_FILE_NAME, material.Texture.FileName);
        }

        [TestMethod]
        public void ParseMaterialNoColorOrTexture()
        {
            string name = "name";
            string xml = String.Format("<material name='{0}'></material>", name); // No sub-element

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Material material = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(name, material.Name);
            Assert.IsNull(material.Color);
            Assert.IsNull(material.Texture);
        }
    }
}
