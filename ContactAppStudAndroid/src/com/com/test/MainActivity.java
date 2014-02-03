package com.com.test;

import java.io.FileOutputStream;
import java.util.ArrayList;
import java.util.HashMap;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;
import com.com.test.R;
import com.com.test.ImageDownloader;
import com.com.test.ImageDownloader.ImageLoaderListener;
import android.app.ListActivity;
import android.app.ProgressDialog;
import android.content.Intent;
import android.graphics.Bitmap;
import android.os.AsyncTask;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.ImageView.ScaleType;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.ListAdapter;
import android.widget.ListView;
import android.widget.ProgressBar;
import android.widget.SimpleAdapter;
import android.widget.TextView;

public class MainActivity extends ListActivity implements View.OnClickListener {
 
    private ProgressDialog pDialog;

    // URL to get contacts JSON
    private static String url = "http://www.appstud.me/test_recrutement/contacts.json";
	
	private TextView percent;
    // JSON Node names
    private static final String TAG_CONTACTS = "contacts";
    private static final String TAG_NOM = "nom";
    private static final String TAG_PRENOM = "prenom";
    private static final String TAG_MAIL = "email";
    private static final String TAG_PICTURE = "picture";
	
	private ImageView img;
	private ProgressBar pb;
	private Button refresh;
	private ImageDownloader mDownloader;
	private static Bitmap bmp;
    // contacts JSONArray
    JSONArray contacts = null;
    Controller aController;
    // Hashmap for ListView
    ArrayList<HashMap<String, String>> contactList;
 
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        initViews();
        aController = (Controller) getApplicationContext();
        contactList = new ArrayList<HashMap<String, String>>(); 
        ListView lv = getListView();
    	
        // Listview on item click listener
        lv.setOnItemClickListener(new OnItemClickListener() {
 
            @Override
            public void onItemClick(AdapterView<?> parent, View view,
                    int position, long id) {         
 
                // Starting single contact activity
                Intent in = new Intent(getApplicationContext(),	ContactDetailActivity.class);
                in.putExtra("id", Integer.toString(position));            
                startActivity(in);
 
            }
            
        });
 
        // Calling async task to get json
        if ( aController.getContactssArraylistSize() == 0){
        new GetContacts().execute();
        }
        else{
        	
        	UpdateContactViewList();
        }
      
    }
 
	private void initViews() {
		
		percent = (TextView) findViewById(R.id.tvPercent);
		percent.setVisibility(View.INVISIBLE);
		refresh = (Button) findViewById(R.id.refresh);
		/*--- we are using 'this' because our class implements the OnClickListener ---*/
		refresh.setOnClickListener(this);
		img = (ImageView) findViewById(R.id.MainImage);
		img.setScaleType(ScaleType.CENTER_CROP);
		pb = (ProgressBar) findViewById(R.id.pbDownload);
		pb.setVisibility(View.INVISIBLE);
	
	}
	
	@Override
	public void onClick(View v) {
		/*--- determine which button was clicked ---*/
		switch (v.getId()) {

		case R.id.refresh:
	
	/*--- check whether there is some Text entered ---*/
			
			/*--- instantiate our downloader passing it required components ---*/
			mDownloader = new ImageDownloader(aController.getContact(0).getPicture(), pb, img, percent, MainActivity.this, bmp, new ImageLoaderListener() {
				@Override
				public void onImageDownloaded(Bitmap bmp) {
					MainActivity.bmp = bmp;
	     /*--- here we assign the value of bmp field in our Loader class 
	               * to the bmp field of the current class ---*/	
				}
				});
			
			/*--- we need to call execute() since nothing will happen otherwise ---*/
			mDownloader.execute();

			break;

		}

	}
    public void UpdateContactViewList()
    {
        int i =0;
        while ( i < aController.getContactssArraylistSize() ){
        
            HashMap<String, String> contact = new HashMap<String, String>();     
            contact.put(TAG_NOM, aController.getContact(i).getNom());
            contact.put(TAG_PRENOM, aController.getContact(i).getPrenom());
            contact.put(TAG_MAIL, aController.getContact(i).getMail());
            contact.put(TAG_PICTURE, aController.getContact(i).getPicture());
            contactList.add(contact);
            
	

            i++;
        }
        
            ListAdapter adapter = new SimpleAdapter(
          MainActivity.this, contactList,
           R.layout.list_contact_item, new String[] { TAG_NOM, TAG_PRENOM,
              TAG_MAIL }, new int[] { R.id.listnom,
             R.id.listprenom, R.id.listmail });
            setListAdapter(adapter);
    	
    }
    /**
     * Async task class to get json by making HTTP call
     * */
    private class GetContacts extends AsyncTask<Void, Void, Void> {
 
        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            // Showing progress dialog
            pDialog = new ProgressDialog(MainActivity.this);
            pDialog.setMessage("Please wait...");
            pDialog.setCancelable(false);
            pDialog.show();
 
        }
 
        @Override
        protected Void doInBackground(Void... arg0) {
            // Creating service handler class instance
            ServiceHandler sh = new ServiceHandler();
 
            // Making a request to url and getting response
            String jsonStr = sh.makeServiceCall(url, ServiceHandler.GET);
 
            Log.d("Response: ", "> " + jsonStr);
 
            if (jsonStr != null) {
                try {
                    JSONObject jsonObj = new JSONObject(jsonStr);
                     
                    // Getting JSON Array node
                    contacts = jsonObj.getJSONArray(TAG_CONTACTS);
 
                    // looping through All Contacts
                    for (int i = 0; i < contacts.length(); i++) {
                        JSONObject c = contacts.getJSONObject(i);                         
                    
                        String nom = c.getString(TAG_NOM);
                        String prenom = c.getString(TAG_PRENOM);
                        String mail = c.getString(TAG_MAIL);
                        String picture = c.getString(TAG_PICTURE);
                        // tmp hashmap for single contact
                     
                        ModelContact contact = new ModelContact(nom,prenom,mail,picture);
                    
                        aController.AddContact(contact);
                    }
                }
                catch (JSONException e) {
                    e.printStackTrace();
                }
            } else {
                Log.e("ServiceHandler", "Couldn't get any data from the url");
            }
 
            return null;
        }
 
        @Override
        protected void onPostExecute(Void result) {
            super.onPostExecute(result);
            // Dismiss the progress dialog
            if (pDialog.isShowing())
                pDialog.dismiss();
            /**
             * Updating parsed JSON data into ListView
             * */
            
            UpdateContactViewList();
    }
    }
}