package serpis.ad;

import javax.persistence.Entity;


@Entity
public class Categoria {
	private Long id;
	private String nombre;
	
	@Id
	public Long getId() {
		return id;
	}
	 
	private void setId(Long id){
		 this.id = id;
	}

    public String getNombre() {
		return nombre;
    }
    
	private void setNombre(String nombre){
		 this.nombre = nombre;
	}

}
