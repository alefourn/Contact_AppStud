package com.com.test;

public class ModelContact {
	
	private String Nom;
	private String Prenom;
	private String Mail;
	private String Picture;
    public ModelContact(String nom,String prenom,String mail,String picture)
    {
    	this.Nom  = nom;
    	this.Prenom  = prenom;
    	this.Mail = mail;
    	this.Picture = picture;
    }
	
	public String getNom() {
		
		return Nom;
	}
   
    public String getPrenom() {
		
		return Prenom;
	}
	
    public String getMail() {
		
		return Mail;
	}
    public String getPicture() {
		
		return Picture;
	}	
}
