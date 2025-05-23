using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class TileBreakableSystem : MonoBehaviour
{
    public Tilemap tilemap;  // Reference to the Tilemap
    public AudioClip breakSound;  // Optional: Sound effect when tile breaks
    public GameObject breakEffectPrefab;  // Optional: Visual effect on tile break
    private Dictionary<Vector3Int, TileState> tileDurabilityDictionary = new Dictionary<Vector3Int, TileState>();

    private void Start()
    {
        InitializeTileStates();
    }

    private void InitializeTileStates()
    {
        int numberOfPositionsWithtin = 0;

        foreach (var position in tilemap.cellBounds.allPositionsWithin)
        {
            // TileBase is the base class for all TileMaps in UnityEngine.Tilemaps, GetTile is a method from TileBase class
            TileBase tile = tilemap.GetTile(position);

            if (tile != null /*&& tile is BreakableTile*/)
            {
                numberOfPositionsWithtin++;
                // Initialize tile state with max health
                tileDurabilityDictionary[position] = new TileState { maxDurability = 10, currentDurability = 10 };
            }
        }

        //Debug.LogWarning(numberOfPositionsWithtin);
        
    }

    public void TryBreakTile(Vector3 attackPosition, int damage)
    {
        //Debug.LogWarning("Am i being hit?");
        Vector3Int cellPosition = tilemap.WorldToCell(attackPosition);  // Convert the attack position to Tilemap cell position

        // Check if the tile is breakable
        if (tileDurabilityDictionary.ContainsKey(cellPosition))  
        {
            TileState tileState = tileDurabilityDictionary[cellPosition];

            tileState.currentDurability -= damage;  // Apply damage to the tile's durability


            //ShowTileFeedback(cellPosition, tileState);

            if (tileState.currentDurability <= 0)
            {
                DestroyTile(cellPosition);
            }
        }
    }

    private void ShowTileFeedback(Vector3Int position, TileState tileState)
    {
        // Play feedback when damage occurs
        if (tileState.currentDurability < tileState.maxDurability)
        {
            // Update visual feedback or play a sound when the tile's durability is reduced
        }
        // Provide warning feedback when the tile durability is low (e.g., less than 20%)
        if (tileState.currentDurability <= tileState.maxDurability * 0.2f)
        {
            // Play a sound or display a visual changes when tile is about to break
        }
    }


    // Destroy the tile and remove it from the Tilemap
    private void DestroyTile(Vector3Int position)
    {
        GameObject particleEffect;
        // Instantiate particle effect at the position the destroyed tile with an offset of
        if (breakEffectPrefab)
        {
            Vector3 worldPos = tilemap.CellToWorld(position);
            particleEffect = Instantiate(breakEffectPrefab, worldPos, Quaternion.identity);
        }

        // Play a break sound
        if (breakSound)
        {
            AudioSource.PlayClipAtPoint(breakSound, tilemap.CellToWorld(position));
        }

        // Remove the tile from the Tilemap
        tilemap.SetTile(position, null);
        
        // Remove the tile state tracking
        tileDurabilityDictionary.Remove(position); 
       
    }

    // Resets all tiles when the level is restarted
    public void ResettileDurabilityDictionary()
    {
        foreach (var position in tileDurabilityDictionary.Keys)
        {
            tileDurabilityDictionary[position] = new TileState { maxDurability = 10, currentDurability = 10 };  // Reset durability
            // tilemap.SetTile(position, originalTile);  // If needed
        }
    }
}

[System.Serializable]
public class TileState
{
    public int maxDurability;
    public int currentDurability;
}