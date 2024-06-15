describe("Testing all Ingredient routes", () => 
{
    const BASE_URL = "http://localhost:5249"

    let Ingredient = {
        name: "Calabresa Portuguesa",
        quantity: 60.5,
        unitPrice: 0.5,
        unit: 'g'
    }

    let updatedIngredient = {
        name: 'Calabresa 2.0'
    }

    it("teste #1: Register an Ingredient", async () => 
    {
        const result = await fetch(BASE_URL+"/ingredients", 
            {
                method: "POST",
                headers : { "Content-Type": "application/json" },
                body: JSON.stringify(Ingredient)
            }
        )
        .then(res => res.json())
        .catch(error => console.log(error))

        Ingredient["id"] = result.id
        
        expect(result.name).toBe(Ingredient.name)
    })
/*
    it("teste #2: Get the newest Ingredient", async () => 
    {
        const result = await fetch(BASE_URL+"/ingredients/"+Ingredient["id"], 
            {method: "GET"}
        )
        .then(response => response.json())
        .catch(error => console.log(error))

        expect(result.id).toBe(Ingredient["id"])
        expect(result.name).toBe(Ingredient.name)
    })    

    it("teste #3: Update the Ingredient", async () => 
    {

        const result = await fetch(BASE_URL+"/ingredients/"+Ingredient["id"],
            {
                method: "PUT",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(updatedIngredient)
            })            
            .then(res => res.json())
            .catch(error => console.log(error)) 
                
        expect(result.name).toBe(updatedIngredient.name)
    })
    
    it("teste #4: Get the Ingredient", async () => 
    {
        const result = await fetch(BASE_URL+"/ingredients/"+Ingredient["id"], 
        {method: "GET"}
        ).then(response => response.json())
        .catch(error => console.log(error))

        expect(result.id).toBe(Ingredient["id"])
        expect(result.name).toBe(updatedIngredient.name)        
    })

    it("teste #5: Delete the Ingredient", async () => 
    {
        const result = await fetch(BASE_URL+"/ingredients/"+Ingredient["id"], 
            {method: "DELETE"}
        )
        .then(response => response.json())
        .catch(error => console.log(error))

        expect(result).toBe(Ingredient["id"])
    })
        */
})