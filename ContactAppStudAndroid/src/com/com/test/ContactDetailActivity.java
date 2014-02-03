package com.com.test;



import android.os.Bundle;
import android.app.Activity;
import android.content.Intent;
import android.view.Menu;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.ImageButton;
import android.widget.ImageView;
import android.widget.TextView;

public class ContactDetailActivity extends Activity {
	int i  = 0;
    Controller aController;
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		aController = (Controller) getApplicationContext();        
		setContentView(R.layout.activity_contact_detail);
		// getIntent() is a method from the started activity
		Intent myIntent = getIntent(); // gets the previously created intent
		String id = myIntent.getStringExtra("id"); // will return "FirstKeyValue"
		i=  Integer.parseInt(id);
		
		final TextView nom = (TextView) findViewById(R.id.nom);
		final TextView prenom = (TextView) findViewById(R.id.prenom);
		final TextView mail = (TextView) findViewById(R.id.mail);
		final TextView picture = (TextView) findViewById(R.id.picture);
		final ImageView image = (ImageView) findViewById(R.id.image);
		final ImageButton deletebtn = (ImageButton) findViewById(R.id.deletebutton);
		
nom.setText(aController.getContact(i).getNom());
prenom.setText(aController.getContact(i).getPrenom());
mail.setText(aController.getContact(i).getMail());
picture.setText(aController.getContact(i).getPicture());

deletebtn.setOnClickListener(new OnClickListener() {
	public void onClick(View v) {
		
		aController.RemoveContact(aController.getContact(i));
		Intent i = new Intent(getBaseContext(), MainActivity.class);
		startActivity(i);
	}
});	

	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.contact_detail, menu);
		return true;
	}

}
