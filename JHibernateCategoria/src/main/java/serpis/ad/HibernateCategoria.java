package serpis.ad;

import javax.persistence.EntityManager;
import javax.persistence.EntityManagerFactory;
import javax.persistence.Persistence;


public class HibernateCategoria {
	
	//Los métodos setup y teardown se ejecutan justo antes y después de los métodos de test.
	//Debemos introducir la creación del entityManagerFactory en el setup y el close() en teardown.

	public static void main(String[] args) {
		EntityManagerFactory entityManagerFactory = 
				Persistence.createEntityManagerFactory("serpis.ad.jpa.mysql");
		entityManagerFactory.close();

	}

}
