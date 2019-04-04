using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using AssetRegister.Attributes;
using AssetRegister.Attributes.AssetTag;
using AssetRegister.Attributes.AssetType;
using AssetRegister.Attributes.Discipline;
using AssetRegister.Attributes.Event;
using AssetRegister.Attributes.Location;
using AssetRegister.Attributes.PhysicalTags;

namespace AssetRegister
{
	public class AssetCollection : List<Asset>
	{
		public List<string> AssetTypes { get; set; }
		public List<string> AssetDisciplines { get; set; }

		private AssetCollection()
		{

		}
		public AssetCollection(DataTable dt)
		{
			AssetCollection atc = new AssetCollection();

			var enumerableData = dt.AsEnumerable();			

			List<string> duplicatedAssetTags=null;
			List<string> registerAssetTypes = null;
			List<string> registerDisciplines = null;

			foreach (DataRow row in dt.Rows)
			{
				var asset = new Asset();
				bool assetLocated = false;
				bool hasMakeModel = false;
	

				foreach (DataColumn col in dt.Columns)
				{
					var colTitle = col.ToString();


					switch (colTitle)
					{
						case "Asset Name":
						{
							if (duplicatedAssetTags == null)
							{
								duplicatedAssetTags = enumerableData.Select(dr => dr.Field<string>(colTitle)).GroupBy(x => x)
									.Where(g => g.Count() > 1).Select(g => g.Key).ToList();
							}


							if (duplicatedAssetTags.Contains(row[colTitle].ToString()))
							{
								asset.AssetId = new DuplicatedAssetTag(row[colTitle].ToString());
							}
							else
							{
								asset.AssetId = new NormalAssetTag(row[colTitle].ToString());
							}

							break;
						}

						case "Asset Type":
						{
							if (registerAssetTypes == null)
							{
								registerAssetTypes = enumerableData.Select(dr => dr.Field<string>(colTitle)).GroupBy(x => x).Select(g => g.Key)
									.ToList();
								atc.AssetTypes = registerAssetTypes;
							}

							asset.AssetType = new AssetTypeObject(row[colTitle].ToString());
								break;
						}
						case "Asset Description":
						{
							asset.Description = row[colTitle].ToString();
							break;
						}

						case "Discipline":
						{
							if (registerDisciplines == null)
							{
								registerDisciplines = enumerableData.Select(dr => dr.Field<string>(colTitle)).GroupBy(x => x).Select(g => g.Key)
									.ToList();

								atc.AssetDisciplines = registerDisciplines;
							}

							asset.Discipline = new DisciplineObject(row[colTitle].ToString());
								break;
						}

						case "Location":
						case "Level":
						case "Room Number":
						{
							if (assetLocated)
							{
								break;
							}

							string buildingColumnName = null;
							string levelColumnName = null;
							string roomColumnName = null;

							if (row["Location"] != null)
							{
								buildingColumnName = "Location";
							}

							if (row["Level"] != null)
							{
								levelColumnName = "Level";
							}

							if (row["Room Number"] != null)
							{
								roomColumnName = "Room Number";
							}

							if (buildingColumnName != null && levelColumnName != null && roomColumnName != null)
							{
								asset.Location = new BuildingLevelRoom(row[buildingColumnName].ToString(), row[levelColumnName].ToString(),
									row[roomColumnName].ToString());
							}

							assetLocated = true;
							break;
						}
						case "Barcode":
						case "Serial No.":
						{
							string value = row[colTitle].ToString();
							if (!string.IsNullOrWhiteSpace(value))
							{
								asset.PhysicalTags.Add(new BarcodeObject(value));
							}

							break;
						}
						case "Make":
						case "Model":
						{
							if (hasMakeModel)
							{
								break;
							}

							string makeColumnName = null;
							string modelColumnName = null;

							if (row["Make"] != null)
							{
								makeColumnName = "Make";
							}

							if (row["Model"] != null)
							{
								modelColumnName = "Model";
							}

							string make = row[makeColumnName].ToString();
							string model = row[modelColumnName].ToString();

							if (!string.IsNullOrWhiteSpace(make) && !string.IsNullOrWhiteSpace(model))
							{
								asset.MakeModel = new MakeModel(make, model);
							}

							hasMakeModel = true;
							break;
						}
						case "Maintenance Date":
						case "PC Date":
						{
							string value = row[colTitle].ToString();
							if (!string.IsNullOrWhiteSpace(value))
							{
								asset.EventLog.Add(new EventObject(Convert.ToDateTime(value), colTitle));
							}

							break;
						}
						case "Warranty Start Date":
						case "Warranty End Date":
						{
							string value = row[colTitle].ToString();
							if (!string.IsNullOrWhiteSpace(value))
							{
								asset.EventLog.Add(new EventObject(Convert.ToDateTime(row[colTitle]), colTitle));
								asset.Warranty.AddDetails(new KeyValuePair<string, string>(colTitle, value));
							}

							break;
						}
						case "Asset Weight":
						{
							string value = row[colTitle].ToString();
							if (!string.IsNullOrWhiteSpace(value))
							{
								asset.Weight = new WeightString(value);
								}
						

							break;
						}
						case "Warranty Period":
						case "Warranty Coverage (Labour/Parts)":
						{
							string value = row[colTitle].ToString();
							if (!string.IsNullOrWhiteSpace(value))
							{
								if (asset.Warranty == null)
								{
									asset.Warranty = new WarrantyBasic();
								}

								if (asset.Warranty.GetType() == typeof(WarrantyBasic))
								{
									asset.Warranty.AddDetails(new KeyValuePair<string, string>(colTitle, value));
								}
							}

							break;
						}
						default:
						{
							var value = row[colTitle].ToString();
							if (!string.IsNullOrWhiteSpace(value))
							{
								asset.OtherAttributes.Add(new OtherAttribute(colTitle, row[colTitle].ToString()));
							}

							break;
						}
					}
				}

				atc.Add(asset);

			}



		}

		
	}
}