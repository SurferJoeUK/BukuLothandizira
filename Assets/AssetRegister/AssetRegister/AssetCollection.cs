using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Xml.XPath;
using AssetRegister.Attributes;
using AssetRegister.Attributes.AssetCategory;
using AssetRegister.Attributes.AssetTag;
using AssetRegister.Attributes.Discipline;
using AssetRegister.Attributes.Event;
using AssetRegister.Attributes.Location;
using AssetRegister.Attributes.MakeModel;
using AssetRegister.Attributes.PhysicalTags;
using AssetRegister.Attributes.Supplier;
using AssetRegister.Attributes.Warranty;
using AssetRegister.Attributes.Weight;
using AssetRegister.Helpers;

namespace AssetRegister
{
	
	public class AssetCollection:IXmlSerializable
	{
		public List<string> AssetTypes { get; set; }
		public List<string> AssetDisciplines { get; set; }
		public List<Asset> Assets { get; set; }

		private AssetCollection() 
		{
			// parameterless constructor for Xml
		}

		public AssetCollection(DataTable dt)
		{
			//AssetCollection atc = new AssetCollection();

			var enumerableData = dt.AsEnumerable();

			List<string> duplicatedAssetTags = null;
			List<string> registerAssetTypes = null;
			List<string> registerDisciplines = null;

			foreach (DataRow row in dt.Rows)
			{
				// quick sanity check, is the row empty?
				bool emptyrow = DataHelper.IsEmpty(row);
				if (emptyrow)
				{
					continue;					
				}


				var asset = new Asset(Guid.NewGuid());
				//asset.AssetId= ;
				
				bool assetLocated = false;
				bool hasMakeModel = false;


				foreach (DataColumn col in dt.Columns)
				{
					var colTitle = col.ToString().Trim();
					var colValue = row[colTitle].ToString().GetRealText();

					if (string.IsNullOrWhiteSpace(colValue))
					{
						continue;
					}

					switch (colTitle.ToUpper())
					{
						#region case: AssetTagDetail
						case "ASSET DESIGNATION":
						case "DESIGNATION":
						case "ASSET NAME":
						{
							if (duplicatedAssetTags == null)
							{
								duplicatedAssetTags = enumerableData.Select(dr => dr.Field<string>(colTitle)?.Trim()).GroupBy(x => x)
									.Where(g => g.Count() > 1).Select(g => g.Key).Where(g=>g!=null).ToList();								
							}

							//var tag = rowx[colTitle].ToString().Trim();

							if (duplicatedAssetTags.Contains(colValue))
							{

								asset.AssetTagDetail = new DuplicatedAssetTag(colValue);
							}
							else
							{
								if (StringHelper.IsADescriptionNotATag(colValue))
								{
									asset.AssetTagDetail = new DescriptiveAssetTag(colValue);
								}
								else
								{
									asset.AssetTagDetail = new NormalAssetTag(colValue);
								}
							}

							break;
						}

						#endregion case: AssetTagDetail

						#region case: AssetType

						case "ASSET CATEGORY CODE":
						case "ASSET TYPE":
						{
							if (registerAssetTypes == null)
							{
								registerAssetTypes = enumerableData.Select(dr => dr.Field<string>(colTitle)).GroupBy(x => x).Select(g => g.Key)
									.ToList();
								this.AssetTypes = registerAssetTypes;
							}

							//asset.AssetCategory = new AssetCategoryObject(rowx[colTitle].ToString());							
							asset.AssetCategory = new AssetCategoryObject(colValue);
							break;
						}

						#endregion case: AssetType

						#region case: AssetDescription

						case "ASSET DESCRIPTION":
						case "DESCRIPTION":
						{
							//asset.Description = rowx[colTitle].ToString();
							asset.Description = colValue;
							break;
						}

						#endregion case: AssetDescription

						#region case: Discipline

						case "DISCIPLINE":
						{
							if (registerDisciplines == null)
							{
								registerDisciplines = enumerableData.Select(dr => dr.Field<string>(colTitle)).GroupBy(x => x).Select(g => g.Key)
									.ToList();

								this.AssetDisciplines = registerDisciplines;
							}

							//asset.Discipline = new DisciplineObject(rowx[colTitle].ToString());
							asset.Discipline = new DisciplineObject(colValue);
							break;
						}

						#endregion case: Discipline

						#region case: Location

						case "LOCATION":
						case "LEVEL":
						case "ROOM NUMBER":
						{
							if (assetLocated)
							{
								break;
							}

							string buildingColumnName = null;
							string levelColumnName = null;
							string roomColumnName = null;

							if (dt.Columns.Contains("Location"))
							{
								buildingColumnName = "Location";
							}

							if (dt.Columns.Contains("Level"))
							{
								levelColumnName = "Level";
							}

							if (dt.Columns.Contains("Room Number"))
							{
								roomColumnName = "Room Number";
							}

							if (buildingColumnName != null && levelColumnName != null && roomColumnName != null)
							{
								asset.Location = new BuildingLevelRoom(
									row[buildingColumnName].ToString().GetRealText(), 
									row[levelColumnName].ToString().GetRealText(),
									row[roomColumnName].ToString().GetRealText());
							}
							else if (buildingColumnName != null)
							{
								asset.Location = new SimpleLocation(row[buildingColumnName].ToString().GetRealText());
							}

							assetLocated = true;
							break;
						}

						#endregion case: Location

						#region case: PhysicalTags

						case "BARCODE":
						case "SERIAL NO.":
						case "ASSET SERIAL NUMBER":
						case "SERIAL NUMBER":
						case "SERIAL":
						{
							asset.InitialisePhysicalTags();
							asset.PhysicalTags.Add(new PhysicalTagObject(colTitle, colValue));

							break;
						}

						#endregion case: PhysicalTags

						#region case: MakeModel

						case "MAKE":
						case "ASSET MAKE":
						case "MODEL":
						case "ASSET MODEL":
						case "ASSET MODEL / DISCRIPTION":
						case "ASSET MODEL / DESCRIPTION":
						case "ASSET MODEL (COLOUR)":
						{
							string[] makeColNameList =
							{
								"MAKE", "ASSET MAKE"
							};

							string[] modelColNameList =
							{
								"MODEL",
								"ASSET MODEL",
								"ASSET MODEL / DISCRIPTION",
								"ASSET MODEL / DESCRIPTION",
								"ASSET MODEL (COLOUR)"
							};

							if (hasMakeModel)
							{
								break;
							}


							#region what it was

							//foreach (var s in makeColNameList)
							//{
							//	if (dt.Columns.Contains(s))
							//	{
							//		makeColumnName = s;
							//		break;
							//	}
							//}

							#endregion

							string makeColumnName = makeColNameList.FirstOrDefault(s => dt.Columns.Contains(s));

							#region what it was

							//	foreach (var s in modelColNameList)
							//{
							//	if (dt.Columns.Contains(s))
							//	{
							//		modelColumnName = s;
							//		break;
							//	}
							//}

							#endregion

							string modelColumnName = modelColNameList.FirstOrDefault(s => dt.Columns.Contains(s));


							string make = null;
							string model = null;

							if (makeColumnName != null)
							{
								make = row[makeColumnName].ToString();
							}

							if (modelColumnName != null)
							{
								model = row[modelColumnName]?.ToString();
							}

							if (!string.IsNullOrWhiteSpace(make) || !string.IsNullOrWhiteSpace(model))
							{
								asset.MakeModel = new MakeModel(make.GetRealText(), model.GetRealText());
							}

							hasMakeModel = true;
							break;
						}

						#endregion case: MakeModel

						#region case: Dates

						case "MAINTENANCE DATE":
						case "PC DATE":
						case "INSTALLATION DATE":
						{
							asset.InitialiseEventLog();
							asset.AddToEventLog(colValue, colTitle);
							break;
						}

						#endregion case: Dates

						#region case: Warranty Dates

						case "WARRANTY START DATE":
						case "WARRANTY END DATE":
						{
							asset.InitialiseEventLog();
							asset.AddToEventLog(colValue, colTitle, true);
							break;
						}

						#endregion case: Warranty Dates

						#region case: Weight

						case "ASSET WEIGHT":
						{
							asset.Weight = new WeightString(colValue);
							break;
						}

						#endregion case: Weight
						#region case: Supplier
						case "ASSET SUPPLIER":
						case "SUPPLIER":
						{
							asset.Supplier = new Supplier(colValue);
							break;
						}

						#endregion case: Supplier
						#region case: Warranty Details

						case "WARRANTY PERIOD":
						{
							string value = colValue.FilterText();
							asset.Warranty = WarrantyHelper.GetWarranty(colTitle, value);

							asset.InitialiseEventLog();

							if (asset.Warranty.GetType() == typeof(WarrantyWithType))
							{
								asset.AddToEventLog_ProjectedEvent(((WarrantyWithType) asset.Warranty).WarrantyPeriod, colTitle, true);
							}
							else
							{
								asset.AddToEventLog_ProjectedEvent(value, colTitle, true);
							}

							break;
						}
						case "WARRANTY COVERAGE (LABOUR/PARTS)":
						{
							asset.InitialiseWarranty();
							asset.Warranty.AddDetails(colTitle, colValue);

							break;
						}

						case "SERVICE / MAINTENACE":
						case "SERVICE / MAINTENANCE":
						case "SERVICE/ MAINTENANCE":
						case "SERVICE MAINTENANCE":
						{
							const string sandm = "ServiceMaintenance";
							if (colValue == "Yearly")
							{
								asset.InitialiseEventLog();
								asset.AddToEventLog_ReoccurringEvent(ReoccurringEvent.TimePeriod.Yearly, sandm, true);
							}
							else if (colValue == "Quarterly")
							{
								asset.InitialiseEventLog();
								asset.AddToEventLog_ReoccurringEvent(ReoccurringEvent.TimePeriod.Quarterly, sandm, true);
							}
							else if (colValue == "6 Monthly")
							{
								asset.InitialiseEventLog();
								asset.AddToEventLog_ReoccurringEvent(ReoccurringEvent.TimePeriod.HalfYearly, sandm, true);
							}
							else if (colValue == "Monthly")
							{
								asset.InitialiseEventLog();
								asset.AddToEventLog_ReoccurringEvent(ReoccurringEvent.TimePeriod.Monthly, sandm, true);
							}
							else if ((Constants.Years.Any(colValue.Contains) || int.TryParse(colValue.Trim(), out int noOfYears)) && colValue.Length<10)
							{
								asset.InitialiseEventLog();
								asset.AddToEventLog_ProjectedEvent(colValue, sandm, true);
							}
							else
							{
								asset.InitialiseOtherAttributes();
								asset.AddOtherAttribute("General", colTitle, colValue, row);
							}

							break;
						}

						#endregion case: Warranty Details

						#region default

						default:
						{
							if (!string.IsNullOrWhiteSpace(colValue))
							{
								asset.InitialiseOtherAttributes();
								asset.AddOtherAttribute(colTitle, colValue, row);
							}

							break;
						}

						#endregion default
					}
				}

				if (this.Assets == null)
				{
					this.Assets = new List<Asset>();
				}
				this.Assets.Add(asset);

			}


		}

		public XmlSchema GetSchema()
		{
			return null;
		}

		public void ReadXml(XmlReader reader)
		{
			throw new System.NotImplementedException();
		}

		public void WriteXml(XmlWriter writer)
		{
			foreach (Asset asset in this.Assets)
			{
				writer.WriteStartElement("Asset");
				writer.WriteAttributeString("AssetId", asset.AssetId.ToString());
				asset.AssetTagDetail?.WriteXml(writer, "AssetTagDetail", "AssetTagDetailType");
				asset.AssetCategory?.WriteXml(writer, "AssetCategory");
				writer.WriteElementString("AssetDescription", asset.Description);
				asset.Discipline?.WriteXml(writer, "Discipline", "DisciplineType");
				asset.Location?.WriteXml(writer, "Location", "LocationType");
				asset.WriteXmlPhysicalTags(writer, "PhysicalTag", "PhysicalTagType");
				asset.MakeModel?.WriteXml(writer, "MakeModel");
				asset.Supplier?.WriteXml(writer, "Supplier");
				asset.WriteXmlEventLog(writer, "EventLog", "EventLogType");
				asset.Weight?.WriteXml(writer, "Weight", "WeightType");				
				asset.Warranty?.WriteXml(writer, "WarrantyDetails", "WarrantyDetailsType");
				asset.WriteXmlOtherAttributes(writer, "OtherAttributes", "OtherAttributeType");
				writer.WriteEndElement();
			}

		}
	}
}