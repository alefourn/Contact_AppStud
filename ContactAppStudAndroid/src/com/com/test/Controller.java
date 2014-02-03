package com.com.test;

import java.util.ArrayList;
import android.app.Application;

public class Controller extends Application{
	
	public  ArrayList<ModelContact> contacts = new ArrayList<ModelContact>();


	public ModelContact getContact(int pPosition) {
		
		return contacts.get(pPosition);
	}
	
	public void AddContact(ModelContact contact) {
	   
		contacts.add(contact);
		
	}	
	
	public void RemoveContact(ModelContact contact) {
		   
		contacts.remove(contact);
		
	}
	
   public int getContactssArraylistSize() {
		
		return contacts.size();
	}
   
}
