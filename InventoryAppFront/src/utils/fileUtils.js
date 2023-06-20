import * as FileSystem from 'expo-file-system';
import * as IntentLauncher from 'expo-intent-launcher';
import cfg from "../configuration.js"


export async function downloadPdf(url, nameToSave){ // to do -> auth header and (rewrite,replace) so it can use context
  console.log('downloading ', url, nameToSave)
  try{
      const file = await FileSystem.downloadAsync(cfg.API_SERVER + url,
        FileSystem.cacheDirectory + nameToSave + '.pdf',
        {
          headers : 
          {
            'Content-Type' : 'application/pdf'
          }
        })

      const cUri = await FileSystem.getContentUriAsync(file.uri);

      await IntentLauncher.startActivityAsync("android.intent.action.VIEW", {
          data: cUri,
          flags: 1,
          type: "application/pdf",
      });

  }
  catch(error){
    console.error(error)
  }
    
}