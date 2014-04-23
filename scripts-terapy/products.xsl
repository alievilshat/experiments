<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:terapy="http://www.navitas.nl/2014/Mapper/dbaccess/terapy" xmlns:m="http://www.navitas.nl/2014/Mapper">
  <xsl:template match="/">
    <target>
      <terapy>
        <xsl:for-each select="terapy:table('_products')">
          <product>
            <productid m:left="80" m:top="162">pk:product(<xsl:value-of select="productnumber" />)</productid>
            <xsl:if test="parent != 'NULL'">
              <parentid m:left="-44" m:top="178">fk:product(<xsl:value-of select="parent" />)</parentid>
            </xsl:if>
            <manufacturercode m:left="82" m:top="194">
              <xsl:value-of select="productnumber" />
            </manufacturercode>
            <barcode m:left="26" m:top="316">
              <xsl:value-of select="productnumber" />
            </barcode>
            <categoryid m:left="334" m:top="210">1</categoryid>
            <brandid m:left="212" m:top="226">156</brandid>
            <producttypeid m:left="334" m:top="244">0</producttypeid>
            <taxcategoryid m:left="218" m:top="442">1</taxcategoryid>
            <name m:left="56" m:top="532">
              <xsl:value-of select="name" />
            </name>
            <description m:left="-72" m:top="548">
              <xsl:value-of select="description" />
            </description>
            <metadescription m:left="57" m:top="565">
              <xsl:value-of select="description" />
            </metadescription>
          </product>
        </xsl:for-each>
      </terapy>
    </target>
  </xsl:template>
</xsl:stylesheet>