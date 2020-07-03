package com.schlafenhase.pandemica.data

import androidx.room.Entity
import androidx.room.ForeignKey
import androidx.room.Index
import androidx.room.PrimaryKey

@Entity(tableName = "contact",
    foreignKeys = [
        ForeignKey(entity = Patient::class,
            parentColumns = ["id"],
            childColumns = ["assignedTo"])
    ])
data class Contact (
    @PrimaryKey(autoGenerate = true) val id: Int? = null,
    val name: String,
    val lastName: String,
    val contactID: Int,
    val age: Int,
    val nationality: String,
    val address: String,
    val pathologies: String,
    val email: String,
    val assignedTo: Int
)