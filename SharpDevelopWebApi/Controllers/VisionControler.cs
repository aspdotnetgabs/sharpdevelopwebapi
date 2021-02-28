using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;

namespace SharpDevelopWebApi.Controllers
{

	
	public class VisionController : ApiController
	{
        // Add your Computer Vision subscription key and endpoint
        string subscriptionKey = "7d28cfb8f90547d79edfd384b46c7987";
        string endpoint = "https://westcentralus.api.cognitive.microsoft.com";
        string ANALYZE_LOCAL_IMAGE = @"C:\Users\DRIVE_D\My Pictures\taylor ai vision.jpg"; 
        
        /*
         * AUTHENTICATE
         * Creates a Computer Vision client used by each example.
         */
        private static ComputerVisionClient Authenticate(string endpoint, string key)
        {
            ComputerVisionClient client =
              new ComputerVisionClient(new ApiKeyServiceClientCredentials(key))
              { Endpoint = endpoint };
            return client;
        }        
        
        
		 private async Task<ImageAnalysis> AnalyzeImageLocal(ComputerVisionClient client, string localImage)
		 {
            // Creating a list that defines the features to be extracted from the image. 
            List<VisualFeatureTypes> features = new List<VisualFeatureTypes>()
	        {
	          VisualFeatureTypes.Categories, VisualFeatureTypes.Description,
	          VisualFeatureTypes.Faces, VisualFeatureTypes.ImageType,
	          VisualFeatureTypes.Tags, VisualFeatureTypes.Adult,
	          VisualFeatureTypes.Color, VisualFeatureTypes.Brands,
	          VisualFeatureTypes.Objects
	        };
            
            using (Stream analyzeImageStream = File.OpenRead(localImage))
            {
                // Analyze the local image.
                ImageAnalysis results = await client.AnalyzeImageInStreamAsync(analyzeImageStream, features);
                return results;
            }		 	
		 }
		 
		 
		[HttpGet] 
		public async Task<IHttpActionResult> GetVision()
		{
			ComputerVisionClient client = Authenticate(endpoint, subscriptionKey);
			var res = await AnalyzeImageLocal(client, ANALYZE_LOCAL_IMAGE);
			return Ok(res);
		}
		
		

	}
}