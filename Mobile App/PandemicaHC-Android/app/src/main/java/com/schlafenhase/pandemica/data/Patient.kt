package com.schlafenhase.pandemica.data

import androidx.room.Entity
import androidx.room.Index
import androidx.room.PrimaryKey

@Entity(tableName = "patient")
data class Patient (
    @PrimaryKey(autoGenerate = true) val id: Int? = null,
    val name: String,
    val lastName: String,
    val patientID: Int,
    val age: Int,
    val nationality: String,
    val region: String,
    val pathologies: String,
    val status: String,
    val medication: String,
    val isHospitalized: Boolean,
    val isInUCI: Boolean
)