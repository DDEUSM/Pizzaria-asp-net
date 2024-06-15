describe("Testing all routes", () => 
{
    const BASE_URL = "http://localhost:5249/pizzas"

    let Pizza = {
        name: "Labresa",
        description: "Pizza saborosa de Queijo Mussarela com Calabresa",
        sizeCategory: "family",
        ingredients: [
            {                
                ingredientId: '5069818E-BDA7-45E5-9C4D-6D47B03FF8DA',
                quantity: 100,
                unit: 'g'
            },
            {
                ingredientId: '10D47164-F61F-4208-8FD7-257C468C07EE',
                quantity: 95,
                unit: 'g' 
            }    
        ],
        price: 56.5,
        productionCost: 22.4,
        discount: 0.5,
        quantity: 21
    }

    let Pizza2  = {
        name: "Calabresa 2.0",
        description: "Pizza saborosa 2.0",
        sizeCategory: "family",
        ingredients: [
            {
                id: 'fcef8cc8-fef1-436c-a9ce-a34518e29ccb',               
                quantity: 50,
                unit: 'g'
            },    
            {
                id: 'fcef8cc8-fef1-436c-a9ce-a34518e29cca',
                quantity: 50,
                unit: 'g'
            },
        ],
        price: 50.5,
        productionCost: 20.4,
        discount: 0,
        quantity: 10
    }

    it("teste #1: Create a Pizza", async () => 
    {
        const result = await fetch(BASE_URL, 
            {
                method: "POST",
                headers : { "Content-Type": "application/json" },
                body: JSON.stringify(Pizza)
            }
        )
        .then(res => res.json())
        .catch(error => console.log(error))

        Pizza["id"] = result.id
        
        expect(result.name).toBe(Pizza.name)
        expect(result.description).toBe(Pizza.description)
    })
/*
    it("teste #2: Get the newest Pizza", async () => 
    {
        const result = await fetch(BASE_URL+"/"+Pizza["id"], 
            {method: "GET"}
        )
        .then(response => response.json())
        .catch(error => console.log(error))

        expect(result.id).toBe(Pizza["id"])
    })    

    it("teste #3: Update the Pizza", async () => 
    {    

        const result = await fetch(BASE_URL+"/"+Pizza["id"],
            {
                method: "PUT",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(Pizza2)
            })            
            .then(res => res.json())
            .catch(error => console.log(error))   
        
        expect(result.name).toBe(Pizza2.name)
        expect(result.ingredients[0].name).toBe(Pizza2.ingredients[0].name)        
    })

    it("teste #4: Get Updated Pizza", async () => 
    {
        const result = await fetch(BASE_URL+"?"+new URLSearchParams({
                name: Pizza2.name,
                description: Pizza2.description,
            })).then(res => res.json())
            .catch(error => console.log(error))
        
        expect(result[0].name).toBe(Pizza2.name)
        expect(result[0].sizeCategory).toBe(Pizza2.sizeCategory)
    })

    it("teste #5: Delete the Pizza", async () => 
    {
        const result = await fetch(BASE_URL+"/"+Pizza["id"], 
            {method: "DELETE"}
        )
        .then(response => response.json())
        .catch(error => console.log(error))
        
        expect(result).toBe(Pizza["id"])
    })

    */
})