namespace SIRSS.Domain.Infrastructure
{
    /* I used these classes because incase we we want to add data anotations to the Entities
     * and if we try to update the real database the whole data annotation
     * will be gone and we will end up writing it again. So instead we put the annotation here. 
     * These classes's properies must be exactly the same with the created PETAEntities but you 
     * dont have to put all the properties, only which you need. below are some examples...
        
     * Also have a look at the Entitis partial classes on EntitiesPartial.class --->  
     */
    
    //public class UserLoginMetaData
    //{
    //    // Composite primary key
    //    // All the three columns make one unique row
    //    // NB. The Order number does not matter
    //    [Key]
    //    [Column(Order = 1)]
    //    public string LoginProvider { get; set; }
    //    [Key]
    //    [Column(Order = 2)]
    //    public string ProviderKey { get; set; }
    //    [Key]
    //    [Column(Order = 3)]
    //    public long UserId { get; set; }
    //}

    /* You can Add more*/

}
