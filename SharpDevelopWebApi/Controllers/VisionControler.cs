using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;

namespace SharpDevelopWebApi.Controllers
{

	
	public class VisionController : ApiController
	{
		
        // Add your Computer Vision subscription key and endpoint
        string subscriptionKey = "PUT YOUR API KEY HERE";
        string endpoint = "PUT YOUR COMPUTER VISION API ENDPOINT HERE";
        
		// AUTHENTICATE. Creates a Computer Vision client
        private static ComputerVisionClient Authenticate(string endpoint, string key)
        {
            ComputerVisionClient client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(key))
            { 
            	Endpoint = endpoint
            };
            
            return client;
        }

        private static string CreateDescription(ImageAnalysis imgAnalysis)
        {
        	var imageDesc = string.Empty;
            foreach (var category in imgAnalysis.Categories)
            {
                if (category.Detail != null && category.Detail.Landmarks != null)
                {
                    foreach (var landmark in category.Detail.Landmarks)
                    {
                    	imageDesc += " I found a landmark " + landmark.Name;
                    }
                }
                
                if (category.Detail != null && category.Detail.Celebrities != null)
                {
                    foreach (var celeb in category.Detail.Celebrities)
                    {
                    	imageDesc += celeb.Name + " is in this photo. ";
                    }
                }              
            }
            
			foreach (var caption in imgAnalysis.Description.Captions)
            {
				imageDesc += " In summary, the photo is about " + caption.Text;
            }        

			return imageDesc;
        }
		
        
		/// <summary>
		/// Retrieves a specific product by unique id
		/// </summary>        
		[HttpPost]
		[FileUpload.SwaggerForm()]
		public async Task<IHttpActionResult> UploadImage(bool asJson = false)
		{
			var postedFile = HttpContext.Current.Request.Files[0];
			var imgByteArray = postedFile.ToImageByteArray();
			
			ComputerVisionClient client = Authenticate(endpoint, subscriptionKey);
			
            // Creating a list that defines the features to be extracted from the image. 
            List<VisualFeatureTypes> features = new List<VisualFeatureTypes>()
	        {
				VisualFeatureTypes.Categories, 
				VisualFeatureTypes.Description
	        };
            
            // Analyze image
            using (Stream analyzeImageStream = new MemoryStream(imgByteArray))
            {
                // Analyze the local image.
                ImageAnalysis results = await client.AnalyzeImageInStreamAsync(analyzeImageStream, features);
                
                if(asJson)
                	return Ok(results); 
               	else
               	{
               		var imgDesc = CreateDescription(results);
               		return Ok(imgDesc);
               	}
            }
		}
		
     
               		


	}
}